using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OperatorManagementDL.Models
{
    public class PersonMD
    {
        [Required(ErrorMessage = "First Name cannot be empty")]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First Name must be between 3 to 20 characters")]
        public string Fld_Person_Fname { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 to 30 characters")]
        public string Fld_Person_Lname { get; set; }
    }

}
