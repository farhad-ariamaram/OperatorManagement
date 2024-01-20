using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// گرفتن لیست کل اشخاص
        /// </summary>
        /// <returns>List<PersonDTO></returns>
        List<PersonDTO> GetPeople();

        /// <summary>
        /// گرفتن اشخاص برای دراپ دان
        /// </summary>
        /// <returns>List<PersonDropdownDTO></returns>
        List<PersonDropdownDTO> GetPeopleForDropdown();

        /// <summary>
        /// گرفتن لیست اشخاص حذف شده
        /// </summary>
        /// <returns>List<PersonDTO></returns>
        List<PersonDTO> GetDeletedPeople();

        /// <summary>
        /// گرفتن شخص با آی دی
        /// </summary>
        /// <param name="personId">آی دی شخص</param>
        /// <returns>PersonDTO</returns>
        PersonDTO GetPersonById(int personId);

        /// <summary>
        /// افزودن شخص جدید
        /// </summary>
        /// <param name="person">شخص</param>
        void AddPerson(PersonDTO person);

        /// <summary>
        /// ویرایش اطلاعات شخص
        /// </summary>
        /// <param name="person">شخص</param>
        /// <returns>PersonDTO</returns>
        PersonDTO UpdatePerson(PersonDTO person);

        /// <summary>
        /// حذف شخص با آی دی
        /// </summary>
        /// <param name="personId">آی دی شخص</param>
        void DeletePersonById(int personId);

        /// <summary>
        /// بازگرداندن شخص حذف شده با آی دی
        /// </summary>
        /// <param name="personId">آی دی شخص</param>
        void UnDeletePersonById(int personId);

    }
}
