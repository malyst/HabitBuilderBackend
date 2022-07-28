using HabitBuilder_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace HabitBuilder_Backend.Areas.Identity.Data
{

    // Add profile data for application users by adding properties to the AppUser class

    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Points { get; set; }
       
     //   public ICollection <UserHabit> UserHabits { get; set; }
       // public ICollection<Reward> Rewards { get; set; }
    }
}
