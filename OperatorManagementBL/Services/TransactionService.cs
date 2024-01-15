using OperatorManagementBL.DTOs;
using OperatorManagementBL.Enum;
using OperatorManagementDL;
using System;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly OperatorManagementDBEntities _context;
        public TransactionService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public List<TransactionDTO> GetTransactions()
        {
            var p = _context.Tbl_Transaction;
            List<TransactionDTO> ret = new List<TransactionDTO>();
            foreach (var item in p)
            {
                ret.Add(new TransactionDTO
                {
                    Id = item.Fld_Transaction_Id,
                    Date = item.Fld_Transaction_Date,
                    FromSimNumber = item.Tbl_Sim.Fld_Sim_Number,
                    ToSimNumber = item.Tbl_Sim1.Fld_Sim_Number,
                    TransactionType = item.Tbl_TransactionType.Fld_TransactionType_Type,
                    Duration = (item.Fld_Transaction_End.HasValue ? item.Fld_Transaction_End.Value : new System.TimeSpan(0)).Subtract(item.Fld_Transaction_Start)
                });
            }

            return ret;
        }

        //ok = 0
        //insuffient balance = 1
        public int MakeCall(int from, int to, int type, int duration)
        {
            try
            {
                var fromSim = _context.Tbl_Sim.Find(from);

                //check balance
                var walletBallance = fromSim.Tbl_Wallet.Fld_Wallet_Balance;
                if(walletBallance <= 0)
                {
                    return 1;
                }

                //تعرفه برای تماس هر دقیقه 5 تومان و برای هر اس ام اس 5 تومان در نظر گرفته شد
                var requiredBalance = 0;
                if(type == (int)TransactionTypeEnum.call)
                {
                    requiredBalance = duration * 5;
                }
                else if (type == (int)TransactionTypeEnum.sms)
                {
                    requiredBalance = 5;
                }
                else
                {
                    throw new Exception();
                }

                var currentDate = DateTime.Now;
                var currentDateDuration = DateTime.Now.AddMinutes(duration);
                TimeSpan currentTime = new TimeSpan(currentDate.Hour, currentDate.Minute, currentDate.Second);
                TimeSpan currentDurationTime = new TimeSpan(currentDateDuration.Hour, currentDateDuration.Minute, currentDateDuration.Second);
                Tbl_Transaction p = new Tbl_Transaction
                {
                    Fld_Sim_FromSimId = from,
                    Fld_Sim_ToSimId = to,
                    Fld_Transaction_Date = currentDate,
                    Fld_Transaction_Start = currentTime,
                    Fld_Transaction_End = currentDurationTime,
                    Fld_TransactionType_Id = type
                };
                _context.Tbl_Transaction.Add(p);
                _context.SaveChanges();

                return 0;
            }
            catch
            {
                throw new Exception("makingCallProblem");
            }
        }
    }
}
