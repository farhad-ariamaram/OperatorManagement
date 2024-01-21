using OperatorManagementBL.DTOs;
using OperatorManagementBL.Exceptions;
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

        #region Priv8
        //------------Priv8 Methods----------//

        private IQueryable<Tbl_Sim> _Get()
        {
            return _context.Tbl_Sim.Where(a => !a.Fld_Sim_IsDeleted && !a.Tbl_Person.Fld_Person_IsDeleted);
        }

        private Tbl_Sim _Get(int simcardId)
        {
            var simcard = _context.Tbl_Sim.Find(simcardId);
            if (simcard == null) { throw new SimcardNotFoundException(); }
            return simcard;
        }

        private void _Add(Tbl_Sim simcard)
        {
            try
            {
                _context.Tbl_Sim.Add(simcard);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("خطای نامشخص در _Add");
            }
        }

        private void _Update(Tbl_Sim simcard)
        {
            try
            {
                _context.Entry(simcard).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("خطای نامشخص در _Update");
            }
        }

        //------------Priv8 Methods----------//
        #endregion

        #region Public
        public List<SimDTO> GetSims()
        {
            var simcards = _Get();

            //مپ لیست سیمکارت ها
            List<SimDTO> mappedSimcards = new List<SimDTO>();
            foreach (var s in simcards)
            {
                mappedSimcards.Add(new SimDTO
                {
                    Id = s.Fld_Sim_Id,
                    Number = s.Fld_Sim_Number,
                    Person_Id = s.Fld_Person_Id,
                    IsActive = s.Fld_Sim_IsActive,
                    SimType_Id = s.Fld_SimType_Id
                });
            }

            return mappedSimcards;
        }

        public List<SimForListDTO> GetSimsForDropdown(int? exclude)
        {
            var simcards = _Get().Where(a => a.Fld_Sim_Id != exclude.Value);

            //مپ لیست سیمکارت ها برای دراپ دان
            List<SimForListDTO> mappedSimcards = new List<SimForListDTO>();
            foreach (var s in simcards)
            {
                mappedSimcards.Add(new SimForListDTO
                {
                    Id = s.Fld_Sim_Id,
                    Number = s.Fld_Sim_Number,
                });
            }

            return mappedSimcards;
        }

        public List<SimDetailDTO> GetDetailSims()
        {
            var simcards = _Get();

            //مپ لیست سیمکارت ها با جزئیات
            List<SimDetailDTO> mappedSimcards = new List<SimDetailDTO>();
            foreach (var s in simcards)
            {
                mappedSimcards.Add(new SimDetailDTO
                {
                    Id = s.Fld_Sim_Id,
                    Number = s.Fld_Sim_Number,
                    Person = s.Tbl_Person.Fld_Person_Fname + " " + s.Tbl_Person.Fld_Person_Lname,
                    IsActive = s.Fld_Sim_IsActive ? "فعال" : "غیرفعال",
                    SimType = s.Tbl_SimType.Fld_SimType_Value,
                    SimTypeId = s.Fld_SimType_Id
                });
            }

            return mappedSimcards;
        }

        public SimDTO GetSimById(int simcardId)
        {
            var simcard = _Get(simcardId);

            //مپ سیمکارت برای نمایش تکی
            SimDTO mappedSimcard = new SimDTO
            {
                Id = simcard.Fld_Sim_Id,
                Number = simcard.Fld_Sim_Number,
                Person_Id = simcard.Fld_Person_Id,
                IsActive = simcard.Fld_Sim_IsActive,
                SimType_Id = simcard.Fld_SimType_Id
            };
            return mappedSimcard;
        }

        public SimDetailDTO GetSimDetailById(int simcardId)
        {
            var simcard = _Get(simcardId);

            //مپ سیمکارت نمایش تکی با جزئیات
            SimDetailDTO mappedSimcard = new SimDetailDTO
            {
                Id = simcard.Fld_Sim_Id,
                Number = simcard.Fld_Sim_Number,
                Person = simcard.Tbl_Person.Fld_Person_Fname + " " + simcard.Tbl_Person.Fld_Person_Lname,
                IsActive = simcard.Fld_Sim_IsActive ? "فعال" : "غیرفعال",
                SimType = simcard.Tbl_SimType.Fld_SimType_Value,
                SimTypeId = simcard.Fld_SimType_Id,
                Balance = simcard.Fld_Sim_Balance
            };
            return mappedSimcard;
        }

        public void AddSim(SimDTO simcardDto)
        {
            //چک کردن تکراری نبودن شماره و ایجاد خطا در صورت تکراری بودن
            var sim = _context.Tbl_Sim.Where(a => a.Fld_Sim_Number == simcardDto.Number);
            if (sim.Any())
            {
                throw new DuplicateSimNumberException();
            }

            //مپ کردن سیمکارت برای ایجاد در جدول با اعتبار اولیه صفر
            Tbl_Sim simcard = new Tbl_Sim
            {
                Fld_Sim_Number = simcardDto.Number,
                Fld_Person_Id = simcardDto.Person_Id,
                Fld_Sim_IsActive = simcardDto.IsActive,
                Fld_SimType_Id = simcardDto.SimType_Id,
                Fld_Sim_Balance = 0m
            };

            _Add(simcard);
        }

        public void UpdateSim(SimDTO simcard)
        {
            //چک کردن تکراری نبودن شماره و ایجاد خطا در صورت تکراری بودن
            var sim = _context.Tbl_Sim.Where(a => a.Fld_Sim_Id != simcard.Id && a.Fld_Sim_Number == simcard.Number);
            if (sim.Any())
            {
                throw new DuplicateSimNumberException();
            }

            //آپدیت مشخصات سیمکارت
            var simForUpdate = _Get(simcard.Id);
            simForUpdate.Fld_Sim_Id = simcard.Id;
            simForUpdate.Fld_Sim_Number = simcard.Number;
            simForUpdate.Fld_Person_Id = simcard.Person_Id;
            simForUpdate.Fld_Sim_IsActive = simcard.IsActive;
            simForUpdate.Fld_SimType_Id = simcard.SimType_Id;

            _Update(simForUpdate);
        }

        public void DeleteSimById(int simcardId)
        {
            Tbl_Sim simcard = _Get(simcardId);

            //تغییر وضعیت سیمکارت به حذف شده
            simcard.Fld_Sim_IsDeleted = true;

            _Update(simcard);
        }

        public List<SimDetailDTO> GetDeletedSims()
        {
            //گرفتن لیست سیمکارت های حذف شده از دیتابیس
            var simcards = _context.Tbl_Sim.Where(a => a.Fld_Sim_IsDeleted);

            //مپ کردن برای نمایش
            List<SimDetailDTO> mappedSimcards = new List<SimDetailDTO>();
            foreach (var s in simcards)
            {
                mappedSimcards.Add(new SimDetailDTO
                {
                    Id = s.Fld_Sim_Id,
                    Number = s.Fld_Sim_Number,
                    Person = s.Tbl_Person.Fld_Person_Fname + " " + s.Tbl_Person.Fld_Person_Lname,
                    SimType = s.Tbl_SimType.Fld_SimType_Value
                });
            }

            return mappedSimcards;
        }

        public void UnDeleteSimById(int simcardId)
        {

            Tbl_Sim simcard = _Get(simcardId);

            //ایجاد خطا در صورت حذف بودن مالک سیمکارت
            if (simcard.Tbl_Person.Fld_Person_IsDeleted)
            {
                throw new SimcardOwnerIsDeletedException();
            }

            //تغییر وضعیت سیمکارت
            simcard.Fld_Sim_IsDeleted = false;

            _Update(simcard);
        }

        public List<SimTypeDTO> GetSimTypes()
        {
            //گرفتن لیست انواع سیمکارت از دیتابیس
            var simTypes = _context.Tbl_SimType;

            //مپ کردن برای نمایش
            List<SimTypeDTO> mappedSimTypes = new List<SimTypeDTO>();
            foreach (var s in simTypes)
            {
                mappedSimTypes.Add(new SimTypeDTO { Id = s.Fld_SimType_Id, Type = s.Fld_SimType_Value });
            }

            return mappedSimTypes;
        }

        public WalletDTO GetWallet(int simcardId)
        {
            var simcard = _Get(simcardId);

            //مپ سیمکارت و اعتبار
            var mappedWallet = new WalletDTO
            {
                Id = simcard.Fld_Sim_Id,
                Balance = simcard.Fld_Sim_Balance,
                Number = simcard.Fld_Sim_Number,
                Person = simcard.Tbl_Person.Fld_Person_Fname + " " + simcard.Tbl_Person.Fld_Person_Lname,
                SimTypeId = simcard.Fld_SimType_Id
            };

            return mappedWallet;
        }

        public void ChargeSim(int simcardId, decimal addBalance)
        {
            var simcard = _Get(simcardId);

            //افزودن به اعتبار سیمکارت
            simcard.Fld_Sim_Balance += addBalance;

            _Update(simcard);
        }

        public void PayBillSim(int simId)
        {
            var simcard = _Get(simId);

            //صفر کردن اعتبار سیمکارت
            simcard.Fld_Sim_Balance = 0.00M;

            _Update(simcard);
        }
        #endregion

    }
}
