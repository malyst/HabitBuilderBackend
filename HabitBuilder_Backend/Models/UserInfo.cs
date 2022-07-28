

namespace HabitBuilder_Backend.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int Points { get; set; }
    }
}
