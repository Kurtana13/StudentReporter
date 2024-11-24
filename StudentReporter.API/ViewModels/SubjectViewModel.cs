using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class SubjectViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be a positive number.")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Subject name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Teacher name cannot be longer than 100 characters.")]
        public string TeacherName { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}
