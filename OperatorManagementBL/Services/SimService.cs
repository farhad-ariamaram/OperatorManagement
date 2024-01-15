using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;

namespace OperatorManagementBL.Services
{
    public class SimService : ISimService
    {
        private readonly OperatorManagementDBEntities _context;
        public SimService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public List<SimDTO> GetSims()
        {
            var p = _context.Tbl_Sim;
            List<SimDTO> ret = new List<SimDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimDTO { Id = item.Fld_Sim_Id, Number = item.Fld_Sim_Number, Person_Id = item.Fld_Person_Id });
            }

            return ret;
        }
        public SimDTO GetSimById(int simId)
        {
            try
            {
                var p = _context.Tbl_Sim.Find(simId);
                SimDTO ret = new SimDTO
                {
                    Id = p.Fld_Sim_Id,
                    Number = p.Fld_Sim_Number,
                    Person_Id = p.Fld_Person_Id
                };
                return ret;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Sim Not Found");
            }
        }
        public void AddSim(SimDTO sim)
        {
            Tbl_Sim p = new Tbl_Sim
            {
                Fld_Sim_Number = sim.Number,
                Fld_Person_Id = sim.Person_Id
            };
            _context.Tbl_Sim.Add(p);
            _context.SaveChanges();
        }
        public SimDTO UpdateSim(SimDTO sim)
        {
            Tbl_Sim p = new Tbl_Sim
            {
                Fld_Sim_Id = sim.Id,
                Fld_Sim_Number = sim.Number,
                Fld_Person_Id = sim.Person_Id
            };
            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();

            return sim;
        }
        public void DeleteSimById(int simId)
        {
            try
            {
                Tbl_Sim p = _context.Tbl_Sim.Find(simId);
                _context.Tbl_Sim.Remove(p);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Sim cannot be deleted");
            }
        }
    }
}
