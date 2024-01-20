using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ICostService
    {
        /// <summary>
        /// گرفتن لیست تعرفه ها
        /// </summary>
        /// <returns>List<CostDTO></returns>
        List<CostDTO> GetCosts();

        /// <summary>
        /// گرفتن یک تعرفه با آی دی
        /// </summary>
        /// <param name="Id">آی دی تعرفه</param>
        /// <returns>CostDTO</returns>
        CostDTO GetCostById(int Id);

        /// <summary>
        /// ویرایش تعرفه
        /// </summary>
        /// <param name="cost">تعرفه</param>
        void UpdateCost(CostDTO cost);
    }
}
