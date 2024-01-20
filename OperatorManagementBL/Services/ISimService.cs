using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ISimService
    {
        /// <summary>
        /// لیست کل سیمکارت ها
        /// </summary>
        /// <returns>List<SimDTO></returns>
        List<SimDTO> GetSims();

        /// <summary>
        /// لیست سیمکارت ها برای دراپ دان
        /// </summary>
        /// <param name="exclude">آی دی سیمکارت که از لیست حذف میشود</param>
        /// <returns>List<SimForListDTO></returns>
        List<SimForListDTO> GetSimsForDropdown(int? exclude);

        /// <summary>
        /// لیست سیمکارت ها همراه با جزئیات
        /// </summary>
        /// <returns>List<SimDetailDTO></returns>
        List<SimDetailDTO> GetDetailSims();

        /// <summary>
        /// گرفتن سیمکارت با آی دی
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        /// <returns>SimDTO</returns>
        SimDTO GetSimById(int simId);

        /// <summary>
        /// گرفتن سیمکارت با آی دی به همراه جزئیات
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        /// <returns>SimDetailDTO</returns>
        SimDetailDTO GetSimDetailById(int simId);

        /// <summary>
        /// اضافه کردن سیمکارت جدید
        /// </summary>
        /// <param name="sim">سیمکارت</param>
        void AddSim(SimDTO sim);

        /// <summary>
        /// ویرایش اطلاعات سیمکارت
        /// </summary>
        /// <param name="sim">سیمکارت</param>
        /// <returns>SimDTO</returns>
        void UpdateSim(SimDTO sim);

        /// <summary>
        /// حذف سیمکارت
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        void DeleteSimById(int simId);

        /// <summary>
        /// گرفتن لیست انواع سیمکارت
        /// </summary>
        /// <returns>List<SimTypeDTO></returns>
        List<SimTypeDTO> GetSimTypes();

        /// <summary>
        /// بازگردانی سیمکارت از حذف با آی دی
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        void UnDeleteSimById(int simId);

        /// <summary>
        /// گرفتن لیست سیمکارت های حذف شده
        /// </summary>
        /// <returns>List<SimDTO></returns>
        List<SimDetailDTO> GetDeletedSims();

        /// <summary>
        /// گرفتن مشخصات مالی یک سیمکارت
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        /// <returns>WalletDTO</returns>
        WalletDTO GetWallet(int simId);

        /// <summary>
        /// شارژ سیمکارت اعتباری با آی دی
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        /// <param name="addBalance">مقدار شارژ</param>
        void ChargeSim(int simId, decimal addBalance);

        /// <summary>
        /// پرداخت قبض سیمکارت دائمی با آی دی
        /// </summary>
        /// <param name="simId">آی دی سیمکارت</param>
        void PayBillSim(int simId);

    }
}
