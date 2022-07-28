using HabitBuilder_Backend.Areas.Identity.Data;
using HabitBuilder_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitBuilder_Backend.Services
{
    public class RewardService : IRewardService

    {
        private readonly UserDBContext _context;
        public  RewardService(UserDBContext context)
            
            {
            _context = context;
            }
        
        public async Task<IEnumerable<Reward>> AddRewardAsync(AppUser user,UserDBContext _context)
        {
            return await _context.Rewards.Where(x => x.Id == user.Id).ToListAsync();
        }

        public async Task<bool> AddRewardAsync(Reward reward, AppUser user)
        {
            reward.Id = user.Id;
            
            _context.Rewards.Add(reward);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
