namespace StudentReporter.API.Model
{
    public class AttendanceSummary
    {
        public int Id { get; set; }

        // Foreign Key to Students
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        // Foreign Key to Subjects
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public int TotalAttendances { get; set; }
    }
}
