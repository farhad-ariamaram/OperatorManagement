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
        private IEnumerable<Tbl_TransactionType> _Get()
        {
            return _context.Tbl_TransactionType;
        }
        private Tbl_TransactionType _Get(int costId)
        {
            var cost = _context.Tbl_TransactionType.Find(costId);
            if (cost == null) { throw new Exception("تعرفه پیدا نشد"); }
            return cost;
        }
        private void _Update(Tbl_TransactionType cost)
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
                Id = cost.Fld_TransactionType_Id,
                Description = cost.Fld_TransactionType_Description,
                TransactionType = cost.Fld_TransactionType_Type,
                Value = cost.Fld_TransactionType_Cost
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
                    Id = c.Fld_TransactionType_Id,
                    TransactionType = c.Fld_TransactionType_Type,
                    Value = c.Fld_TransactionType_Cost,
                    Description = c.Fld_TransactionType_Description
                });
            }

            return mappedCosts;
        }

        public void UpdateCost(CostDTO costDto)
        {
            //آپدیت تعرفه
            var cost = _Get(costDto.Id);
            cost.Fld_TransactionType_Cost = costDto.Value;

            _Update(cost);
        }
        #endregion

    }
}
