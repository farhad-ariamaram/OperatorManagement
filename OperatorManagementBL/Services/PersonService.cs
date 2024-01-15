using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Collections.Generic;
using System.Data.Entity;

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
            var p = _context.Tbl_Person;
            List<PersonDTO> ret = new List<PersonDTO>();
            foreach (var item in p)
            {
                ret.Add(new PersonDTO { Id = item.Fld_Person_Id, FirstName = item.Fld_Person_Fname, LastName = item.Fld_Person_Lname });
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
                    LastName = p.Fld_Person_Lname
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
            Tbl_Person p = new Tbl_Person
            {
                Fld_Person_Fname = person.FirstName,
                Fld_Person_Lname = person.LastName
            };
            _context.Tbl_Person.Add(p);
            _context.SaveChanges();
        }
        public PersonDTO UpdatePerson(PersonDTO person) 
        {
            Tbl_Person p = new Tbl_Person
            {
                Fld_Person_Id = person.Id,
                Fld_Person_Fname = person.FirstName,
                Fld_Person_Lname = person.LastName
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
                _context.Tbl_Person.Remove(p);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Person cannot be deleted");
            }
        }
    }
}
