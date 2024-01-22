using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "نام کاربری نمیتواند خالی باشد")]
        [Display(Name = "نام کاربری")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "نام کاربری باید بین 5 تا 50 کاراکتر باشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = "کلمه عبور نمیتواند خالی باشد")]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "کلمه عبور باید بین 6 تا 100 کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "تکرار کلمه عبور نمیتواند خالی باشد")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "تکرار کلمه عبور باید بین 6 تا 100 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه عبور و تکرار آن مغایرت دارند")]
        public string RePassword { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ایمیل نمیتواند خالی باشد")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "ایمیل باید بین 6 تا 100 کاراکتر باشد")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="ایمیل معتبر نمی‌باشد")]
        public string Email { get; set; }

        [Display(Name = "وضعیت غیرفعالی کاربر")]
        public bool IsLocked { get; set; }

        [Display(Name = "متعلق به")]
        public int? PersonId { get; set; }

        [Display(Name = "متعلق به")]
        public string Person { get; set; }

    }
}
