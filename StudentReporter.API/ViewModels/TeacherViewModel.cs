using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class TeacherViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Teacher ID must be a positive number.")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Teacher's name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        // A list of subjects that the teacher is teaching
        public List<SubjectViewModel> Subjects { get; set; }
    }
}
