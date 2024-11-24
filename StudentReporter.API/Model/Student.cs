namespace StudentReporter.API.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public virtual ICollection<StudentWeekDay>? StudentWeekDay { get; set; }
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; }
        public virtual ICollection<AttendanceSummary>? AttendanceSummaries { get; set; }

    }
}
