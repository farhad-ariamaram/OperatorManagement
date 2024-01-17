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

        public List<PersonDTO> GetPeople()
        {
            var p = _context.Tbl_Person.Where(a=>!a.Fld_Person_IsDeleted);
            List<PersonDTO> ret = new List<PersonDTO>();
            foreach (var item in p)
            {
                ret.Add(new PersonDTO { Id = item.Fld_Person_Id, FirstName = item.Fld_Person_Fname, LastName = item.Fld_Person_Lname, NationCode = item.Fld_Person_NationCode });
            }

            return ret;
        }

        public List<PersonDropdownDTO> GetPeopleForDropdown()
        {
            var p = _context.Tbl_Person.Where(a => !a.Fld_Person_IsDeleted);
            List<PersonDropdownDTO> ret = new List<PersonDropdownDTO>();
            foreach (var item in p)
            {
                ret.Add(new PersonDropdownDTO { Id = item.Fld_Person_Id, NameAndNationCode = $"{item.Fld_Person_Fname} {item.Fld_Person_Lname} ({item.Fld_Person_NationCode})" });
            }

            return ret;
        }

        public List<PersonDTO> GetDeletedPeople()
        {
            var p = _context.Tbl_Person.Where(a => a.Fld_Person_IsDeleted);
            List<PersonDTO> ret = new List<PersonDTO>();
            foreach (var item in p)
            {
                ret.Add(new PersonDTO { Id = item.Fld_Person_Id, FirstName = item.Fld_Person_Fname, LastName = item.Fld_Person_Lname, NationCode = item.Fld_Person_NationCode });
            }

            return ret;
        }

        public PersonDTO GetPersonById(int personId)
        {
            try
            {
                var p = _context.Tbl_Person.Find(personId);
                PersonDTO ret = new PersonDTO
                {
                    Id = p.Fld_Person_Id,
                    FirstName = p.Fld_Person_Fname,
                    LastName = p.Fld_Person_Lname,
                    NationCode = p.Fld_Person_NationCode
                };
                return ret;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Person Not Found");
            }
        }
        public void AddPerson(PersonDTO person)
        {
            //check duplicate NationCode
            var pe = _context.Tbl_Person.Where(a => a.Fld_Person_NationCode == person.NationCode).SingleOrDefault();
            if (pe != null)
            {
                throw new DuplicateNationCodeException();
            }

            Tbl_Person p = new Tbl_Person
            {
                Fld_Person_Fname = person.FirstName,
                Fld_Person_Lname = person.LastName,
                Fld_Person_NationCode = person.NationCode
            };
            _context.Tbl_Person.Add(p);
            _context.SaveChanges();
        }
        public PersonDTO UpdatePerson(PersonDTO person) 
        {
            //check duplicate NationCode
            var pe = _context.Tbl_Person.Where(a => a.Fld_Person_NationCode == person.NationCode).SingleOrDefault();
            if (pe != null)
            {
                throw new System.Exception("کدملی تکراری است");
            }

            Tbl_Person p = new Tbl_Person
            {
                Fld_Person_Id = person.Id,
                Fld_Person_Fname = person.FirstName,
                Fld_Person_Lname = person.LastName,
                Fld_Person_NationCode = person.NationCode
            };
            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();

            return person;
        }

        public void DeletePersonById(int personId)
        {
            try
            {
                Tbl_Person p = _context.Tbl_Person.Find(personId);
                p.Fld_Person_IsDeleted = true;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Person cannot be deleted");
            }
        }

        public void UnDeletePersonById(int personId)
        {
            try
            {
                Tbl_Person p = _context.Tbl_Person.Find(personId);
                p.Fld_Person_IsDeleted = false;
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Person cannot be undelete");
            }
        }
    }
}
