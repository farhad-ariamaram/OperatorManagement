using System.ComponentModel.DataAnnotations;

namespace OperatorManagementDL.Models
{
    public class SimMD
    {
        [Required(ErrorMessage = "Sim number cannot be empty")]
        [Display(Name = "Sim Number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Sim number must be between 10 to 15 characters")]
        public string Fld_Sim_Number { get; set; }

        [Required(ErrorMessage = "Person cannot be empty")]
        [Display(Name = "Person")]
        public int Fld_Person_Id { get; set; }

        
    }
}
