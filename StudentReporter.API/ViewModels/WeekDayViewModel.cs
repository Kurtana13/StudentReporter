using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class WeekDayViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "WeekDay ID must be a positive number.")]
        public int Id { get; set; }

        [Required]
        [Range(1, 16, ErrorMessage = "Week must be between 1 and 16.")]
        public int WeekNumber { get; set; } // Represents the week (1-16)
    }
}
