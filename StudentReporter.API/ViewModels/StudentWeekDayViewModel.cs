using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class StudentWeekDayViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive number.")]
        public int StudentId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "WeekDay ID must be a positive number.")]
        public int WeekDayId { get; set; }

        [Required]
        public int WeekNumber { get; set; } // The week number

        [Required]
        [StringLength(100, ErrorMessage = "Student name cannot exceed 100 characters.")]
        public string StudentName { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "Student surname cannot exceed 100 characters.")]
        public string StudentSurname { get; set; } = null!;

        [Required]
        public int? StatusId { get; set; } // Optional status indicating attendance

        [StringLength(50, ErrorMessage = "Status name cannot exceed 50 characters.")]
        public string? StatusName { get; set; } // Name of the status (e.g., "Attended")

    }
}
