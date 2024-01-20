using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO جزئیات سیم‌کارت
    public class SimDetailDTO
    {
        public int Id { get; set; }
        public int SimTypeId { get; set; }

        [Display(Name = "نوع سیم‌کارت")]
        public string SimType { get; set; }

        [Display(Name = "مالک سیم‌کارت")]
        public string Person { get; set; }

        [Display(Name = "شماره سیم‌کارت")]
        public string Number { get; set; }

        [Display(Name = "وضعیت سیم‌کارت")]
        public string IsActive { get; set; }

        public decimal Balance { get; set; }
    }
}
