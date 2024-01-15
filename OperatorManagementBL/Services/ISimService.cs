using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ISimService
    {
        List<SimDTO> GetSims();
        SimDTO GetSimById(int simId);
        void AddSim(SimDTO sim);
        SimDTO UpdateSim(SimDTO sim);
        void DeleteSimById(int simId);
    }
}
