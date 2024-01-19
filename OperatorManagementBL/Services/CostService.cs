using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public class CostService : ICostService
    {
        private readonly OperatorManagementDBEntities _context;
        public CostService()
        {
            _context = new OperatorManagementDBEntities();
        }

        public CostDTO GetCostById(int Id)
        {
            try
            {
                var p = _context.Tbl_Cost.Find(Id);
                if (p==null)
                {
                    throw new System.Exception("تعرفه پیدا نشد");
                }

                return new CostDTO
                {
                    Id = p.Fld_Cost_Id,
                    Description = p.Fld_Cost_Description,
                    TransactionType = p.Tbl_TransactionType.Fld_TransactionType_Type,
                    Value = p.Fld_Cost_Value
                };

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public List<CostDTO> GetCosts()
        {
            var p = _context.Tbl_Cost;
            List<CostDTO> ret = new List<CostDTO>();
            foreach (var c in p) 
            {
                ret.Add(new CostDTO
                {
                    Id = c.Fld_Cost_Id,
                    TransactionType = c.Tbl_TransactionType.Fld_TransactionType_Type,
                    Value = c.Fld_Cost_Value,
                    Description = c.Fld_Cost_Description
                });
            }

            return ret;
        }

        public void UpdateCost(CostDTO cost)
        {
            try
            {
                var p = _context.Tbl_Cost.Find(cost.Id);
                if (p == null) 
                { 
                    throw new Exception("تعرفه یافت نشد");
                }

                p.Fld_Cost_Value = cost.Value;
                _context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
