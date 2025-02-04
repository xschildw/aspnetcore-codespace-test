using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class PasswordSelectViewModel
    {
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Birth year is required")]
        public string BirthYear { get; set; }

        public string FavoriteColor {get; set;} 
        
        // The list of possible passwords to choose from.
        public List<string>? Passwords { get; set; }

        // The password the user selects.
        public string? SelectedPassword { get; set; }
    }
}