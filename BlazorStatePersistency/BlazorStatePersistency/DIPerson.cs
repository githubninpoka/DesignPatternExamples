using System.ComponentModel.DataAnnotations;

namespace BlazorStatePersistency
{
    public class DIPerson
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateOnly? BirthDate { get; set; }
    }
}
