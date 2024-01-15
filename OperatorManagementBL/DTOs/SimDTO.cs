using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class SimDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sim number cannot be empty")]
        [Display(Name = "Sim Number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Sim number must be between 10 to 15 characters")]
        public string Number { get; set; }

        [Display(Name = "Person")]
        public int Person_Id { get; set; }
    }
}
