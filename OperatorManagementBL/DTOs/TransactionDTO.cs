using System;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        [Display(Name ="تاریخ")]
        public DateTime Date { get; set; }

        [Display(Name = "شماره مبدا")]
        public string FromSimNumber { get; set; }

        [Display(Name = "شماره مقصد")]
        public string ToSimNumber { get; set; }

        [Display(Name = "فرد مبدا")]
        public string FromPerson { get; set; }

        [Display(Name = "فرد مقصد")]
        public string ToPerson { get; set; }

        [Display(Name = "نوع")]
        public string TransactionType { get; set; }

        [Display(Name = "مدت تماس")]
        public TimeSpan Duration { get; set; }
    }
}
