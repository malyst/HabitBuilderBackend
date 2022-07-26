using HabitBuilder_Backend.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitBuilder_Backend.Areas.Identity.Data
{
    //Be able to assign roles (User,Admin)
    public class UserDBContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public UserDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
