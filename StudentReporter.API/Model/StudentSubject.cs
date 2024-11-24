namespace StudentReporter.API.Model
{
    public class StudentSubject
    {
        public int StudentId { get; set; } // Composite Key
        public Student Student { get; set; } = null!;

        public int SubjectId { get; set; } // Composite Key
        public Subject Subject { get; set; } = null!;
    }
}
