using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO جزئیات شخص برای نمایش در صفحه جزئیات
    public class PersonDetailDTO
    {
        public int Id { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "کد ملی")]
        public string NationCode { get; set; }

        public List<SimForPersonDTO> SimCards { get; set; }
    }
}
