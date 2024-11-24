namespace StudentReporter.API.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Foreign Key to Teachers
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }


        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<AttendanceSummary> AttendanceSummaries { get; set; }
    }

}

