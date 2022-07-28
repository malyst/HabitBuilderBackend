using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HabitBuilder_Backend.Areas.Identity.Data;
using HabitBuilder_Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HabitBuilder_Backend.Services;

namespace HabitBuilder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class RewardsController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRewardService _rewardService;



        public RewardsController(UserDBContext context, UserManager<AppUser> userManager, IRewardService rewardService)
        {
            _context = context;
            _userManager = userManager;
            _rewardService=rewardService;
        }

        // GET: api/Rewards/View
        [HttpGet("View")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reward>>> GetRewards()
        {
          if (_context.Rewards == null)
          {
              return NotFound();
          }
  
            //Returning the reward to the specfic user.
            var rewards = await _context.Rewards.Where(a => a.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).ToListAsync();
            return rewards;


        }

       

        // POST: api/Rewards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]

        [Authorize]
        public async Task<ActionResult<Reward>> PostReward(Reward reward)
        {
          if (_context.Rewards == null)
          {
              return Problem("Entity set 'UserDBContext.Rewards'  is null.");
          }
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Problem("User is not signed in");
            await _rewardService.AddRewardAsync(reward, currentUser);

          
            return CreatedAtAction("GetRewards", new { id = reward.Id }, reward);
        }

        // DELETE: api/Rewards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            if (_context.Rewards == null)
            {
                return NotFound();
            }
            var reward = await _context.Rewards.FindAsync(id);
            if (reward == null)
            {
                return NotFound();
            }

            _context.Rewards.Remove(reward);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RewardExists(string id)
        {
            return (_context.Rewards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
