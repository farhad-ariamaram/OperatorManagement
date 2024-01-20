using System;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO تراکنش انجام شده 
    public class TransactionDTO
    {
        public int Id { get; set; }

        [Display(Name = "ردیف")]
        public int RowNumber { get; set; }

        [Display(Name ="تاریخ و ساعت")]
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
        public Nullable<TimeSpan> Duration { get; set; }
    }
}
