using OperatorManagementBL.DTOs;
using OperatorManagementDL;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface IPersonService
    {
        List<PersonDTO> GetPeople();
        PersonDTO GetPersonById(int personId);
        void AddPerson(PersonDTO person);
        PersonDTO UpdatePerson(PersonDTO person);
        void DeletePersonById(int personId);

    }
}
