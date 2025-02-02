using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class User
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Birth year is required")]
        public string BirthYear { get; set; }
        
        [Required(ErrorMessage = "Birth year is required")]
        public string FavoriteColor { get; set; }
        // This property will be set by the password service.
        public string? Password { get; set; }
        
    }
}