namespace StudentReporter.API.Model
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<StudentWeekDay>? StudentWeekDays { get; set; }
    }
}
