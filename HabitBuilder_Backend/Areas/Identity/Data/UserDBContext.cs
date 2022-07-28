using HabitBuilder_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitBuilder_Backend.Areas.Identity.Data
{
    //Be able to assign roles (User,Admin)
    public class UserDBContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
        public DbSet<UserHabit> UserHabits { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        

    }

}

