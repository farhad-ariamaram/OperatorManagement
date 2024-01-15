using System;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementDL.Models
{
    public class TransactionMD
    {
        [Display(Name = "Transaction Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime Fld_Transaction_Date { get; set; }

        [Display(Name = "From Sim")]
        public int Fld_Sim_FromSimId { get; set; }

        [Display(Name = "To Sim")]
        public int Fld_Sim_ToSimId { get; set; }

        [Display(Name = "Transaction Type")]
        public int Fld_TransactionType_Id { get; set; }

        [Display(Name = "Start Time")]
        public System.TimeSpan Fld_Transaction_Start { get; set; }

        [Display(Name = "End Time")]
        public Nullable<System.TimeSpan> Fld_Transaction_End { get; set; }
    }
}
