using System.ComponentModel.DataAnnotations;

namespace SearchAndMatch.Application.DTOs
{
    public class PatientCreateRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "The First name must be a string with minimum of 5 caracteres.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        public string DiseaseType { get; set; }
    }
}
