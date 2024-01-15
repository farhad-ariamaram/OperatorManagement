using System.ComponentModel.DataAnnotations;

namespace OperatorManagementDL.Models
{
    public class WalletMD
    {
        [Required(ErrorMessage = "Balance cannot be empty")]
        [Display(Name = "Balance")]
        public decimal Fld_Wallet_Balance { get; set; }
    }
}
