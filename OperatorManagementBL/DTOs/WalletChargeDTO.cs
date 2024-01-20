using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    //DTO شارز سیم‌کارت یا پرداخت قبض
    public class WalletChargeDTO
    {
        public int SimId { get; set; }
        public int SimTypeId { get; set; }

        [Required(ErrorMessage = "مبلغ شارژ معتبر نمی‌باشد")]
        public decimal AddBalance { get; set; }
    }
}
