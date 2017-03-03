using System.ComponentModel.DataAnnotations;

namespace SensieOperationAPI.Models
{
    public class Duration
    {
        public int DurationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}