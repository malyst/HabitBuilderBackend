using HabitBuilder_Backend.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HabitBuilder_Backend.Models
{
    public class Reward
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Cost { get; set; }

       
       // public virtual AppUser AppUser { get; set; } //user rewards will be mapped to userID
    }
}
