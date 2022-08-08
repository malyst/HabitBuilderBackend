using HabitBuilder_Backend.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HabitBuilder_Backend.Models
{
    public class UserHabit
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int HabitId { get; set; }  
        [Required]
        public int DailyCompletion { get; set; }
        [Required]
        public int MonthlyCompletion { get; set; }
        [Required]
        public int YearlyCompletion { get; set; }


    }
}
