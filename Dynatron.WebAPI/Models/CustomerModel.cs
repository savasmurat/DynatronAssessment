using System.ComponentModel.DataAnnotations;

namespace Dynatron.WebAPI.Models
{
    public class CustomerModel
    {
#nullable disable
        public CustomerModel() { }
#nullable enable

        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}
