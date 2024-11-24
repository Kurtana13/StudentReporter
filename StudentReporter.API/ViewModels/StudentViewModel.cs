using System.ComponentModel.DataAnnotations;

namespace StudentReporter.API.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive number.")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Student name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Student surname cannot be longer than 100 characters.")]
        public string Surname { get; set; }

        // List of subjects the student is enrolled in
        public List<SubjectViewModel> Subjects { get; set; }
    }
}
