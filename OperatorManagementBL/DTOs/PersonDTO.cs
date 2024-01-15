using System.ComponentModel.DataAnnotations;

namespace OperatorManagementBL.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty")]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First Name must be between 3 to 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 to 30 characters")]
        public string LastName { get; set; }
    }
}
