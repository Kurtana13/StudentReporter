using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class StatusUpdateViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Status ID must be a positive number.")]
        public int StatusId { get; set; } // E.g., 1 = Attended, 2 = Absent
    }


}
