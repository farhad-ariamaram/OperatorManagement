using System;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class ChargeLogDTO
    {
        [Display(Name = "مبلغ")]
        public decimal Value { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }
    }
}
