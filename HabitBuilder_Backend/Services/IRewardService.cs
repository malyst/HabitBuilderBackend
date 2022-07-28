using HabitBuilder_Backend.Areas.Identity.Data;
using HabitBuilder_Backend.Models;

namespace HabitBuilder_Backend.Services
{
    public interface IRewardService
    {
        Task<bool> AddRewardAsync(Reward reward, AppUser user);


    }
}
