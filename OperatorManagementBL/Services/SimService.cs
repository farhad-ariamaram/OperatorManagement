using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            var p = _context.Tbl_Sim.Where(a=>!a.Fld_Sim_IsDeleted && !a.Tbl_Person.Fld_Person_IsDeleted);
            List<SimDTO> ret = new List<SimDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimDTO { 
                    Id = item.Fld_Sim_Id, 
                    Number = item.Fld_Sim_Number, 
                    Person_Id = item.Fld_Person_Id,
                    IsActive = item.Fld_Sim_IsActive,
                    SimType_Id = item.Fld_SimType_Id
                });
            }

            return ret;
        }

        public List<SimForListDTO> GetSimsForDropdown(int? exclude)
        {
            var p = _context.Tbl_Sim.Where(a => !a.Fld_Sim_IsDeleted && !a.Tbl_Person.Fld_Person_IsDeleted && a.Fld_Sim_Id!=exclude.Value);
            List<SimForListDTO> ret = new List<SimForListDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimForListDTO
                {
                    Id = item.Fld_Sim_Id,
                    Number = item.Fld_Sim_Number,
                });
            }

            return ret;
        }

        public List<SimDetailDTO> GetDetailSims()
        {
            var p = _context.Tbl_Sim.Where(a => !a.Fld_Sim_IsDeleted && !a.Tbl_Person.Fld_Person_IsDeleted);
            List<SimDetailDTO> ret = new List<SimDetailDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimDetailDTO
                {
                    Id = item.Fld_Sim_Id,
                    Number = item.Fld_Sim_Number,
                    Person = item.Tbl_Person.Fld_Person_Fname +" "+ item.Tbl_Person.Fld_Person_Lname,
                    IsActive = item.Fld_Sim_IsActive?"فعال":"غیرفعال",
                    SimType = item.Tbl_SimType.Fld_SimType_Value,
                    SimTypeId = item.Fld_SimType_Id
                });
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
                    Person_Id = p.Fld_Person_Id,
                    IsActive = p.Fld_Sim_IsActive,
                    SimType_Id = p.Fld_SimType_Id
                };
                return ret;
            }
            catch
            {
                throw new System.Exception("Sim Not Found");
            }
        }

        public SimDetailDTO GetSimDetailById(int simId)
        {
            try
            {
                var p = _context.Tbl_Sim.Find(simId);
                SimDetailDTO ret = new SimDetailDTO
                {
                    Id = p.Fld_Sim_Id,
                    Number = p.Fld_Sim_Number,
                    Person = p.Tbl_Person.Fld_Person_Fname +" "+ p.Tbl_Person.Fld_Person_Lname,
                    IsActive = p.Fld_Sim_IsActive?"فعال":"غیرفعال",
                    SimType = p.Tbl_SimType.Fld_SimType_Value,
                    SimTypeId = p.Fld_SimType_Id
                };
                return ret;
            }
            catch
            {
                throw new System.Exception("Sim Not Found");
            }
        }

        public void AddSim(SimDTO sim)
        {
            Tbl_Sim p = new Tbl_Sim
            {
                Fld_Sim_Number = sim.Number,
                Fld_Person_Id = sim.Person_Id,
                Fld_Sim_IsActive = sim.IsActive,
                Fld_SimType_Id = sim.SimType_Id,
            };
            _context.Tbl_Sim.Add(p);
            _context.SaveChanges();

            _context.Tbl_Wallet.Add(new Tbl_Wallet
            {
                Fld_Wallet_Id = p.Fld_Sim_Id,
                Fld_Wallet_Balance = 0
            });
            _context.SaveChanges();
        }

        public SimDTO UpdateSim(SimDTO sim)
        {
            Tbl_Sim p = new Tbl_Sim
            {
                Fld_Sim_Id = sim.Id,
                Fld_Sim_Number = sim.Number,
                Fld_Person_Id = sim.Person_Id,
                Fld_Sim_IsActive = sim.IsActive,
                Fld_SimType_Id = sim.SimType_Id
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
                p.Fld_Sim_IsDeleted = true;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Sim cannot be deleted");
            }
        }

        public List<SimDTO> GetDeletedSims()
        {
            var p = _context.Tbl_Sim.Where(a => a.Fld_Sim_IsDeleted);
            List<SimDTO> ret = new List<SimDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimDTO
                {
                    Id = item.Fld_Sim_Id,
                    Number = item.Fld_Sim_Number,
                    Person_Id = item.Fld_Person_Id,
                    IsActive = item.Fld_Sim_IsActive,
                    SimType_Id = item.Fld_SimType_Id
                });
            }

            return ret;
        }

        public void UnDeleteSimById(int simId)
        {
            try
            {
                Tbl_Sim p = _context.Tbl_Sim.Find(simId);
                p.Fld_Sim_IsDeleted = false;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Sim cannot be undelete");
            }
        }

        public List<SimTypeDTO> GetSimTypes()
        {
            var p = _context.Tbl_SimType;
            List<SimTypeDTO> ret = new List<SimTypeDTO>();
            foreach (var item in p)
            {
                ret.Add(new SimTypeDTO { Id = item.Fld_SimType_Id, Type = item.Fld_SimType_Value });
            }

            return ret;
        }

        public WalletDTO GetWallet(int simId)
        {
            var p = _context.Tbl_Sim.Find(simId);
            if (p == null)
            {
                throw new System.Exception();
            }

            var ret = new WalletDTO
            {
                Id = simId,
                Balance = p.Tbl_Wallet.Fld_Wallet_Balance,
                Number = p.Fld_Sim_Number,
                Person = p.Tbl_Person.Fld_Person_Fname + " " + p.Tbl_Person.Fld_Person_Lname,
                SimTypeId = p.Fld_SimType_Id
            };

            return ret;
        }

        public void ChargeSim(int simId, decimal addBalance)
        {
            try
            {
                var p = _context.Tbl_Sim.Find(simId);
                if (p == null)
                {
                    throw new System.Exception();
                }

                p.Tbl_Wallet.Fld_Wallet_Balance += addBalance;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }

        }

        public void PayBillSim(int simId)
        {
            try
            {
                var p = _context.Tbl_Sim.Find(simId);
                if (p == null)
                {
                    throw new System.Exception();
                }

                p.Tbl_Wallet.Fld_Wallet_Balance = 0.00M;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new System.Exception();
            }
        }
    }
}
