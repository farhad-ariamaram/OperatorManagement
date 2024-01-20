using OperatorManagementBL.DTOs;
using OperatorManagementBL.Exceptions;
using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OperatorManagementBL.Services
{
    public class PersonService : IPersonService
    {
        private readonly OperatorManagementDBEntities _context;
        public PersonService()
        {
            _context = new OperatorManagementDBEntities();
        }

        #region Priv8
        //----------------Priv8 Methods----------------//

        //لیست اشخاص حذف نشده از دیتابیس
        private IQueryable<Tbl_Person> _Get()
        {
            return _context.Tbl_Person.Where(a => !a.Fld_Person_IsDeleted);
        }

        //شخص از دیتابیس با آی دی
        private Tbl_Person _Get(int personId)
        {
            //گرفتن شخص از دیتابیس با آی دی
            var person = _context.Tbl_Person.Find(personId);

            //خطای شخص یافت نشد در صورت پیدا نکردن شخص
            if (person == null) { throw new PersonNotFoundException(); }

            return person;
        }

        //آپدیت شخص
        private void _Update(Tbl_Person person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new System.Exception("خطای نامشخص در UpdatePerson");
            }
        }

        //افزودن شخص
        private void _Add(Tbl_Person person)
        {
            try
            {
                _context.Tbl_Person.Add(person);
                _context.SaveChanges();
            }
            catch
            {
                throw new System.Exception("خطای نامشخص در AddPerson");
            }
        }

        //----------------Priv8 Methods----------------//
        #endregion

        #region Public
        public List<PersonDTO> GetPeople()
        {
            var people = _Get();

            //مپ کردن لیست اشخاص برای نمایش
            List<PersonDTO> mappedPeople = new List<PersonDTO>();
            foreach (var p in people)
            {
                mappedPeople.Add(new PersonDTO { Id = p.Fld_Person_Id, FirstName = p.Fld_Person_Fname, LastName = p.Fld_Person_Lname, NationCode = p.Fld_Person_NationCode });
            }

            return mappedPeople;
        }

        public PersonDetailDTO GetPersonByIdForDetail(int personId)
        {

            var person = _Get(personId);

            //مپ کردن سیمکارت های شخص
            var mappedPersonSimcards = new List<SimForPersonDetailDTO>();
            if (mappedPersonSimcards.Any())
            {
                foreach (var item in person.Tbl_Sim)
                {
                    mappedPersonSimcards.Add(new SimForPersonDetailDTO
                    {
                        Id = item.Fld_Sim_Id,
                        Number = item.Fld_Sim_Number
                    });
                }
            }

            //مپ کردن شخص
            PersonDetailDTO mappedPerson = new PersonDetailDTO
            {
                Id = person.Fld_Person_Id,
                FirstName = person.Fld_Person_Fname,
                LastName = person.Fld_Person_Lname,
                NationCode = person.Fld_Person_NationCode,
                SimCards = mappedPersonSimcards
            };

            return mappedPerson;

        }

        public void AddPerson(PersonDTO personDto)
        {
            //چک کردن تکراری نبودن کد ملی و خطا در صورت تکراری بودن
            var person = _context.Tbl_Person.Where(a => a.Fld_Person_NationCode == personDto.NationCode);
            if (person.Any())
            {
                throw new DuplicateNationCodeException();
            }

            //مپ کردن به مدل
            Tbl_Person mappedPerson = new Tbl_Person
            {
                Fld_Person_Fname = personDto.FirstName,
                Fld_Person_Lname = personDto.LastName,
                Fld_Person_NationCode = personDto.NationCode
            };

            _Add(mappedPerson);
        }

        public PersonDTO GetPersonById(int personId)
        {
            var person = _Get(personId);

            //مپ کردن شخص به DTO
            PersonDTO mappedPerson = new PersonDTO
            {
                Id = person.Fld_Person_Id,
                FirstName = person.Fld_Person_Fname,
                LastName = person.Fld_Person_Lname,
                NationCode = person.Fld_Person_NationCode
            };

            return mappedPerson;
        }

        public void UpdatePerson(PersonDTO personDto)
        {
            //چک کردن تکراری نبودن کد ملی و خطا در صورت تکراری بودن
            var person = _context.Tbl_Person.Where(a => a.Fld_Person_Id != personDto.Id && a.Fld_Person_NationCode == personDto.NationCode);
            if (person.Any())
            {
                throw new DuplicateNationCodeException();
            }

            //آپدیت مشخصات شخص
            var personForUpdate = _Get(personDto.Id);
            personForUpdate.Fld_Person_Fname = personDto.FirstName;
            personForUpdate.Fld_Person_Lname = personDto.LastName;
            personForUpdate.Fld_Person_NationCode = personDto.NationCode;

            _Update(personForUpdate);
        }

        public void DeletePersonById(int personId)
        {
            var person = _Get(personId);

            //تغییر وضعیت کاربر به حذف شده
            person.Fld_Person_IsDeleted = true;

            //تغییر وضعیت سیمکارت های کاربر به حذف شده
            foreach (var item in person.Tbl_Sim)
            {
                item.Fld_Sim_IsDeleted = true;
            }

            _Update(person);
        }

        public List<PersonDTO> GetDeletedPeople()
        {
            //گرفتن لیست اشخاص حذف شده
            var people = _context.Tbl_Person.Where(a => a.Fld_Person_IsDeleted);

            //مپ کردن لیست اشخاص
            List<PersonDTO> mappedPeople = new List<PersonDTO>();
            foreach (var p in people)
            {
                mappedPeople.Add(new PersonDTO { Id = p.Fld_Person_Id, FirstName = p.Fld_Person_Fname, LastName = p.Fld_Person_Lname, NationCode = p.Fld_Person_NationCode });
            }

            return mappedPeople;
        }

        public void UnDeletePersonById(int personId)
        {
            var person = _Get(personId);

            //تغییر وضعیت حذف کاربر
            person.Fld_Person_IsDeleted = false;

            _Update(person);
        }

        public List<PersonDropdownDTO> GetPeopleForDropdown()
        {
            var people = _Get();

            //مپ کردن لیست اشخاص
            List<PersonDropdownDTO> mappedPeople = new List<PersonDropdownDTO>();
            foreach (var p in people)
            {
                mappedPeople.Add(new PersonDropdownDTO { Id = p.Fld_Person_Id, NameAndNationCode = $"{p.Fld_Person_Fname} {p.Fld_Person_Lname} ({p.Fld_Person_NationCode})" });
            }

            return mappedPeople;
        }
        #endregion
    }
}
