using System.ComponentModel.DataAnnotations;

namespace OperatorManagementDL.Models
{
    public class TransactionTypeMD
    {
        [Display(Name = "Transaction Type")]
        public string Fld_TransactionType_Type { get; set; }
    }
}
