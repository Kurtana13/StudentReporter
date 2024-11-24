namespace StudentReporter.API.Model
{
    public class WeekDay
    {
        public int Id { get; set; }
        public int Week { get; set; }

        public virtual ICollection<StudentWeekDay>? StudentWeekDay { get; set; }

    }
}
