using OperatorManagementBL.DTOs;
using OperatorManagementBL.Enum;
using OperatorManagementBL.Extensions;
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

        public TransactionPageDTO GetTransactions(int pageId = 1, long fromDate = 0, long toDate = 0, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0, int sortType = 0, string search = "")
        {
            IQueryable<Tbl_Transaction> q_transactions = _context.Tbl_Transaction;
            TransactionPageDTO ret = new TransactionPageDTO();

            //فیلتر تاریخ بعد از
            if (fromDate != 0)
            {
                DateTime fromDateTime = fromDate.ToGeorgianDateTimeFromUnixTimeStamp();
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Date >= fromDateTime);
            }

            //فیلتر تاریخ قبل از
            if (toDate != 0)
            {
                DateTime toDateTime = toDate.ToGeorgianDateTimeFromUnixTimeStamp();
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Date <= toDateTime);
            }

            //فیلتر از سیمکارت
            if (fromSimId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Sim_FromSimId == fromSimId);
            }

            //فیلتر به سیمکارت
            if (toSimId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Sim_ToSimId == toSimId);
            }

            //فیلتر از شخص
            if (fromPersonId != 0)
            {
                var listPersonSims = _context.Tbl_Sim.Where(a => a.Fld_Person_Id == fromPersonId).Select(a => a.Fld_Sim_Id);
                q_transactions = q_transactions.Where(a => listPersonSims.Contains(a.Fld_Sim_FromSimId));
            }

            //فیلتر به شخص
            if (toPersonId != 0)
            {
                var listPersonSims = _context.Tbl_Sim.Where(a => a.Fld_Person_Id == toPersonId).Select(a => a.Fld_Sim_Id);
                q_transactions = q_transactions.Where(a => listPersonSims.Contains(a.Fld_Sim_ToSimId));
            }

            //فیلتر مدت کمتر از
            if (durationLessThan != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Duration < new TimeSpan(0, durationLessThan, 0));
            }

            //فیلتر مدت بیش تر از
            if (durationMoreThan != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_Transaction_Duration > new TimeSpan(0, durationMoreThan, 0));
            }

            //فیلتر نوع
            if (typeId != 0)
            {
                q_transactions = q_transactions.Where(a => a.Fld_TransactionType_Id == typeId);
            }

            //جستجو
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                q_transactions = q_transactions
                    .Where(a => a.Tbl_Sim.Tbl_Person.Fld_Person_Fname.Contains(search) ||
                                a.Tbl_Sim.Tbl_Person.Fld_Person_Lname.Contains(search) ||
                                a.Tbl_Sim1.Tbl_Person.Fld_Person_Fname.Contains(search) ||
                                a.Tbl_Sim1.Tbl_Person.Fld_Person_Lname.Contains(search));
            }

            //مرتب سازی
            switch (sortType)
            {
                case (int)SortTypeEnum.Newest:
                    q_transactions = q_transactions.OrderByDescending(a => a.Fld_Transaction_Date);
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

            //صفحه بندی
            int take = 10;
            int skip = (pageId - 1) * take;
            ret.ResultCount = q_transactions.Count();
            ret.CurrentPage = pageId;
            ret.PageCount = ret.ResultCount / take;
            if (ret.PageCount * take < q_transactions.Count())
                ret.PageCount++;
            q_transactions = q_transactions.Skip(skip).Take(take);

            //مپ کردن
            int i = 1; // شماره ردیف
            List<TransactionDTO> transactionsList = new List<TransactionDTO>();
            foreach (var item in q_transactions)
            {
                transactionsList.Add(new TransactionDTO
                {
                    Id = item.Fld_Transaction_Id,
                    RowNumber = i + ((ret.CurrentPage - 1) * take),
                    Date = item.Fld_Transaction_Date,
                    FromSimNumber = item.Tbl_Sim.Fld_Sim_Number,
                    ToSimNumber = item.Tbl_Sim1.Fld_Sim_Number,
                    TransactionType = item.Tbl_TransactionType.Fld_TransactionType_Type,
                    Duration = item.Fld_Transaction_Duration,
                    FromPerson = item.Tbl_Sim.Tbl_Person.Fld_Person_Fname + " " + item.Tbl_Sim.Tbl_Person.Fld_Person_Lname,
                    ToPerson = item.Tbl_Sim1.Tbl_Person.Fld_Person_Fname + " " + item.Tbl_Sim1.Tbl_Person.Fld_Person_Lname,
                });
                i++;
            }

            ret.TransactionsList = transactionsList;

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

                //اگر اعتباری باشد و شارژ نداشته باشد
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance <= 0)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //محاسبه مبلغ مورد نیاز برای تماس
                var callCost = _context.Tbl_Cost.Where(a => a.Tbl_TransactionType.Fld_TransactionType_Id == (int)TransactionTypeEnum.call).SingleOrDefault().Fld_Cost_Value;
                var requiredBalance = duration * callCost;

                //اگر اعتباری بود و شارژ کافی نداشت
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance < requiredBalance)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //مپ کردن جهت ثبت تراکنش در دیتابیس
                Tbl_Transaction p = new Tbl_Transaction
                {
                    Fld_Sim_FromSimId = from,
                    Fld_Sim_ToSimId = to,
                    Fld_Transaction_Date = DateTime.Now,
                    Fld_Transaction_Duration = new TimeSpan(0, duration, 0),
                    Fld_TransactionType_Id = type
                };
                _context.Tbl_Transaction.Add(p);
                _context.SaveChanges();

                //بروزرسانی اعتبار
                fromSim.Tbl_Wallet.Fld_Wallet_Balance -= requiredBalance;
                _context.Entry(fromSim).State = EntityState.Modified;
                _context.SaveChanges();

                return 0;
            }
            catch
            {
                return (int)CallFailedEnum.unknown;
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

                //اگر اعتباری باشد و شارژ نداشته باشد
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance <= 0)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //محاسبه اعتبار مورد نیاز ارسال پیام
                var smsCost = _context.Tbl_Cost.Where(a => a.Tbl_TransactionType.Fld_TransactionType_Id == (int)TransactionTypeEnum.sms).SingleOrDefault().Fld_Cost_Value;
                var requiredBalance = smsCost;

                //اگر اعتباری بود و شارژ کافی نداشت
                if (fromSim.Fld_SimType_Id == (int)SimTypeEnum.credit && walletBallance < requiredBalance)
                {
                    return (int)CallFailedEnum.insuffienceBalance;
                }

                //مپ کردن جهت ثبت تراکنش
                Tbl_Transaction p = new Tbl_Transaction
                {
                    Fld_Sim_FromSimId = from,
                    Fld_Sim_ToSimId = to,
                    Fld_Transaction_Date = DateTime.Now,
                    Fld_Transaction_Duration = null,
                    Fld_TransactionType_Id = type
                };
                _context.Tbl_Transaction.Add(p);
                _context.SaveChanges();

                //بروزرسانی اعتبار
                fromSim.Tbl_Wallet.Fld_Wallet_Balance -= requiredBalance;
                _context.Entry(fromSim).State = EntityState.Modified;
                _context.SaveChanges();

                return 0;
            }
            catch
            {
                return (int)CallFailedEnum.unknown;
            }
        }
    }
}
