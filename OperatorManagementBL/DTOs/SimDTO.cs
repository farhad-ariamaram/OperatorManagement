using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO سیم‌کارت
    public class SimDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نوع سیم‌کارت نمیتواند خالی باشد")]
        public int SimType_Id { get; set; }

        [Required(ErrorMessage = "مالک نمیتواند خالی باشد")]
        public int Person_Id { get; set; }

        [Required(ErrorMessage = "شماره نمیتواند خالی باشد")]
        [Display(Name = "شماره سیم‌کارت")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "شماره باید 10 رقم باشد")]
        public string Number { get; set; }

        [Display(Name = "وضعیت سیم‌کارت")]
        public bool IsActive { get; set; }

    }
}
