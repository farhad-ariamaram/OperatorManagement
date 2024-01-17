using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ISimService
    {
        List<SimDTO> GetSims();
        List<SimForListDTO> GetSimsForDropdown(int? exclude);
        List<SimDetailDTO> GetDetailSims();
        SimDTO GetSimById(int simId);
        SimDetailDTO GetSimDetailById(int simId);
        void AddSim(SimDTO sim);
        SimDTO UpdateSim(SimDTO sim);
        void DeleteSimById(int simId);

        List<SimTypeDTO> GetSimTypes();

        void UnDeleteSimById(int simId);
        List<SimDTO> GetDeletedSims();
        WalletDTO GetWallet(int simId);
        void ChargeSim(int simId, decimal addBalance);
        void PayBillSim(int simId);

    }
}
