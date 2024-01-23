using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "نام کاربری نمیتواند خالی باشد")]
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Required(ErrorMessage = "کلمه عبور نمیتواند خالی باشد")]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
