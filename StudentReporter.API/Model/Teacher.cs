namespace StudentReporter.API.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}
