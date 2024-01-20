using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    ////DTO شخص
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام نمیتواند خالی باشد")]
        [Display(Name = "نام")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "نام باید بین 3 تا 20 کاراکتر باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی نمیتواند خالی باشد")]
        [Display(Name = "نام خانوادگی")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین 3 تا 30 کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کد ملی نمیتواند خالی باشد")]
        [Display(Name = "کد ملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        public string NationCode { get; set; }
    }

    public class PersonDetailDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام نمیتواند خالی باشد")]
        [Display(Name = "نام")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "نام باید بین 3 تا 20 کاراکتر باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی نمیتواند خالی باشد")]
        [Display(Name = "نام خانوادگی")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین 3 تا 30 کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کد ملی نمیتواند خالی باشد")]
        [Display(Name = "کد ملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
        public string NationCode { get; set; }

        public List<SimDetailDTO> SimCards { get; set; }
    }
}
