using OperatorManagementBL.DTOs;
using OperatorManagementBL.Enum;
using OperatorManagementDL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OperatorManagementBL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly OperatorManagementDBEntities _context;
        public TransactionService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public List<TransactionDTO> GetTransactions(DateTime? fromDate, DateTime? toDate, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0, int sortType = 0)
        {
            IQueryable<Tbl_Transaction> q_transactions = _context.Tbl_Transaction;

            if (fromDate.HasValue)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Date >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Date <= toDate.Value);
            }

            if (fromSimId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Sim_FromSimId == fromSimId);
            }

            if (toSimId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Sim_ToSimId == toSimId);
            }

            if (fromPersonId != 0)
            {
                var listPersonSims = _context.Tbl_Sim.Where(a => a.Fld_Person_Id == fromPersonId).Select(a => a.Fld_Sim_Id);
                q_transactions = q_transactions.Where(a => listPersonSims.Contains(a.Fld_Sim_FromSimId));
            }

            if (toPersonId != 0)
            {
                var listPersonSims = _context.Tbl_Sim.Where(a => a.Fld_Person_Id == toPersonId).Select(a => a.Fld_Sim_Id);
                q_transactions = q_transactions.Where(a => listPersonSims.Contains(a.Fld_Sim_ToSimId));
            }

            if (durationLessThan != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Duration <= new TimeSpan(0, durationLessThan,0));
            }

            if (durationMoreThan != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Duration >= new TimeSpan(0, durationMoreThan,0));
            }

            if (typeId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_TransactionType_Id == typeId);
            }

            switch (sortType)
            {
                case (int)SortTypeEnum.Newest:
                    q_transactions = q_transactions.OrderByDescending(a=>a.Fld_Transaction_Date);
                    break;
                case (int)SortTypeEnum.Oldest:
                    q_transactions = q_transactions.OrderBy(a => a.Fld_Transaction_Date);
                    break;
                case (int)SortTypeEnum.Longest:
                    q_transactions = q_transactions.OrderByDescending(a => a.Fld_Transaction_Duration);
                    break;
                default:
                    q_transactions = q_transactions.OrderByDescending(a => a.Fld_Transaction_Date);
                    break;
            }

            List<TransactionDTO> ret = new List<TransactionDTO>();
            foreach (var item in q_transactions)
            {
                ret.Add(new TransactionDTO
                {
                    Id = item.Fld_Transaction_Id,
                    Date = item.Fld_Transaction_Date,
                    FromSimNumber = item.Tbl_Sim.Fld_Sim_Number,
                    ToSimNumber = item.Tbl_Sim1.Fld_Sim_Number,
                    TransactionType = item.Tbl_TransactionType.Fld_TransactionType_Type,
                    Duration = item.Fld_Transaction_Duration,
                    FromPerson = item.Tbl_Sim.Tbl_Person.Fld_Person_Fname +" "+ item.Tbl_Sim.Tbl_Person.Fld_Person_Lname,
                    ToPerson = item.Tbl_Sim1.Tbl_Person.Fld_Person_Fname +" "+ item.Tbl_Sim1.Tbl_Person.Fld_Person_Lname,
                });
            }

            return ret;
        }

        public int MakeCall(int from, int to, int type, int duration)
        {
            try
            {
                var fromSim = _context.Tbl_Sim.Find(from);
                var toSim = _context.Tbl_Sim.Find(to);

                //اگر سیمکارت مبدا غیرفعال بود
                if (!fromSim.Fld_Sim_IsActive)
                {
                    return (int)CallFailedEnum.inactiveFromSimcard;
                }

                //اگر سیمکارت مقصد غیرفعال بود
                if (!toSim.Fld_Sim_IsActive)
                {
                    return (int)CallFailedEnum.inactiveToSimcard;
                }

                var walletBallance = fromSim.Tbl_Wallet.Fld_Wallet_Balance;

                //اگر اعتباری باشد و کلا شارژ نداشته باشد
                if(fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance <= 0)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //تعرفه برای تماس هر دقیقه 5 تومان 
                var requiredBalance = duration * 5;

                //اگر اعتباری بود و شارژ کافی نداشت
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance < requiredBalance)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                Tbl_Transaction p = new Tbl_Transaction
                {
                    Fld_Sim_FromSimId = from,
                    Fld_Sim_ToSimId = to,
                    Fld_Transaction_Date = DateTime.Now,
                    Fld_Transaction_Duration = new TimeSpan(0,duration,0),
                    Fld_TransactionType_Id = type
                };
                _context.Tbl_Transaction.Add(p);
                _context.SaveChanges();

                //update balance
                fromSim.Tbl_Wallet.Fld_Wallet_Balance -= requiredBalance;
                _context.Entry(fromSim).State = EntityState.Modified;
                _context.SaveChanges();

                return 0;
            }
            catch
            {
                throw new Exception("makingCallProblem");
            }
        }

        public int SendSMS(int from, int to, int type)
        {
            try
            {
                var fromSim = _context.Tbl_Sim.Find(from);
                var toSim = _context.Tbl_Sim.Find(to);

                //اگر سیمکارت مبدا غیرفعال بود
                if (!fromSim.Fld_Sim_IsActive)
                {
                    return (int)CallFailedEnum.inactiveFromSimcard;
                }

                //اگر سیمکارت مقصد غیرفعال بود
                if (!toSim.Fld_Sim_IsActive)
                {
                    return (int)CallFailedEnum.inactiveToSimcard;
                }

                var walletBallance = fromSim.Tbl_Wallet.Fld_Wallet_Balance;

                //اگر اعتباری باشد و کلا شارژ نداشته باشد
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance <= 0)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //تعرفه برای ارسال هر پیامک 5 تومان 
                var requiredBalance = 5;

                //اگر اعتباری بود و شارژ کافی نداشت
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance < requiredBalance)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                Tbl_Transaction p = new Tbl_Transaction
                {
                    Fld_Sim_FromSimId = from,
                    Fld_Sim_ToSimId = to,
                    Fld_Transaction_Date = DateTime.Now,
                    Fld_Transaction_Duration = new TimeSpan(0, 0, 0),
                    Fld_TransactionType_Id = type
                };
                _context.Tbl_Transaction.Add(p);
                _context.SaveChanges();

                //update balance
                fromSim.Tbl_Wallet.Fld_Wallet_Balance -= requiredBalance;
                _context.Entry(fromSim).State = EntityState.Modified;
                _context.SaveChanges();

                return 0;
            }
            catch
            {
                throw new Exception("sendSMSProblem");
            }
        }
    }
}
