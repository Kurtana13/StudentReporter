namespace StudentReporter.API.Model
{
    public class StudentWeekDay
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int WeekDayId { get; set; }
        public WeekDay WeekDay { get; set; } = null!;

        public int? StatusId { get; set; }
        public Status Status { get; set; }
    }
}
