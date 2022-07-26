using System.ComponentModel.DataAnnotations;

namespace HabitBuilder_Backend.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
