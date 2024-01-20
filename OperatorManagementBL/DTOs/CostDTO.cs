using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO تعرفه
    public class CostDTO
    {
        public int Id { get; set; }

        [Display(Name ="نوع تعرفه")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage ="مقدار تعرفه معتبر نمی‌باشد")]
        [Display(Name ="مقدار تعرفه")]
        [Range(0.01d, (double)decimal.MaxValue, ErrorMessage = "مقدار تعرفه معتبر نمی‌باشد")]
        public decimal Value { get; set; }

        [Display(Name ="توضیحات")]
        public string Description { get; set; }
    }
}
