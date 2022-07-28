using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HabitBuilder_Backend.Areas.Identity.Data;
using HabitBuilder_Backend.Models;

namespace HabitBuilder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHabitsController : ControllerBase
    {
        private readonly UserDBContext _context;

        public UserHabitsController(UserDBContext context)
        {
            _context = context;
        }

        // GET: api/UserHabits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHabit>>> GetUserHabits()
        {
            return await _context.UserHabits.ToListAsync();
        }

        // GET: api/UserHabits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHabit>> GetUserHabit(int id)
        {
            var userHabit = await _context.UserHabits.FindAsync(id);

            if (userHabit == null)
            {
                return NotFound();
            }

            return userHabit;
        }

        // PUT: api/UserHabits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHabit(int id, UserHabit userHabit)
        {
            if (id != userHabit.Id)
            {
                return BadRequest();
            }

            _context.Entry(userHabit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHabitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserHabits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserHabit>> PostUserHabit(UserHabit userHabit)
        {
            _context.UserHabits.Add(userHabit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserHabit", new { id = userHabit.Id }, userHabit);
        }

        // DELETE: api/UserHabits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHabit(int id)
        {
            var userHabit = await _context.UserHabits.FindAsync(id);
            if (userHabit == null)
            {
                return NotFound();
            }

            _context.UserHabits.Remove(userHabit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserHabitExists(int id)
        {
            return _context.UserHabits.Any(e => e.Id == id);
        }
    }
}
