using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ICostService
    {
        List<CostDTO> GetCosts();
        CostDTO GetCostById(int Id);
        void UpdateCost(CostDTO cost);
    }
}
