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

        #region Priv8
        private IEnumerable<Tbl_Cost> _Get()
        {
            return _context.Tbl_Cost;
        }
        private Tbl_Cost _Get(int costId)
        {
            var cost = _context.Tbl_Cost.Find(costId);
            if (cost == null) { throw new Exception("تعرفه پیدا نشد"); }
            return cost;
        }
        private void _Update(Tbl_Cost cost)
        {
            try
            {
                _context.Entry(cost).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("خطا نامشخص در _Update");
            }
        }
        #endregion

        #region Public
        public CostDTO GetCostById(int costId)
        {
            var cost = _Get(costId);

            //مپ کردن تعرفه برای نمایش
            return new CostDTO
            {
                Id = cost.Fld_Cost_Id,
                Description = cost.Fld_Cost_Description,
                TransactionType = cost.Tbl_TransactionType.Fld_TransactionType_Type,
                Value = cost.Fld_Cost_Value
            };
        }

        public List<CostDTO> GetCosts()
        {
            var costs = _Get();

            //مپ کردن لیست تعرفه ها
            List<CostDTO> mappedCosts = new List<CostDTO>();
            foreach (var c in costs)
            {
                mappedCosts.Add(new CostDTO
                {
                    Id = c.Fld_Cost_Id,
                    TransactionType = c.Tbl_TransactionType.Fld_TransactionType_Type,
                    Value = c.Fld_Cost_Value,
                    Description = c.Fld_Cost_Description
                });
            }

            return mappedCosts;
        }

        public void UpdateCost(CostDTO costDto)
        {
            //آپدیت تعرفه
            var cost = _Get(costDto.Id);
            cost.Fld_Cost_Value = costDto.Value;

            _Update(cost);
        }
        #endregion

    }
}
