
using OperatorManagementBL.DTOs;

namespace OperatorManagementBL.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// گرفتن لیست تراکنش ها
        /// </summary>
        /// <param name="pageId">شماره صفحه</param>
        /// <param name="fromDate">فیلتر از تاریخ</param>
        /// <param name="toDate">فیلتر تا تاریخ</param>
        /// <param name="fromSimId">فیلتر از سیمکارت</param>
        /// <param name="toSimId">فیلتر به سیمکارت</param>
        /// <param name="fromPersonId">فیلتر از شخص</param>
        /// <param name="toPersonId">فیلتر به شخص</param>
        /// <param name="durationLessThan"> فیلتر مدت زمان کمتراز (دقیقه)</param>
        /// <param name="durationMoreThan">فیلتر مدت زمان بیشتر از (دقیقه)</param>
        /// <param name="typeId">فیلتر نوع تراکنش</param>
        /// <param name="sortType">نوع مرتب سازی</param>
        /// <param name="search">جستجو</param>
        /// <returns>TransactionPageDTO</returns>
        TransactionPageDTO GetTransactions(int pageId = 1, long fromDate = 0, long toDate = 0, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0, int sortType = 0, string search = "");

        /// <summary>
        /// برقراری تماس
        /// خروجی تابع از نوع CallFailedEnum است
        /// </summary>
        /// <param name="from">آی دی سیمکارت مبدا</param>
        /// <param name="to">آی دی سیمکارت مقصد</param>
        /// <param name="type">نوع تراکنش</param>
        /// <param name="duration">مدت زمان</param>
        /// <returns>int</returns>
        int MakeCall(int from, int to, int type, int duration);

        /// <summary>
        /// فرستادن پیامک
        /// خروجی تابع از نوع CallFailedEnum است
        /// </summary>
        /// <param name="from">آی دی سیمکارت مبدا</param>
        /// <param name="to">آی دی سیمکارت مقصد</param>
        /// <param name="type">نوع تراکنش</param>
        /// <returns>int</returns>
        int SendSMS(int from, int to, int type);
    }
}
