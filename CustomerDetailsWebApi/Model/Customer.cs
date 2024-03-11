using System.ComponentModel.DataAnnotations;

namespace CustomerDetailsWebApi.Model
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        // Add other properties as needed
    }
}
