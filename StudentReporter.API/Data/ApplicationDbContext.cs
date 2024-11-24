using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StudentReporter.API.Model;

namespace StudentReporter.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(b =>
            {
                b.HasKey(x=>x.Id);
            });

            builder.Entity<Teacher>(b =>
            {
                b.HasKey(x => x.Id);
            });

            builder.Entity<Status>(b =>
            {
                b.HasKey(x => x.Id);
            });

            builder.Entity<WeekDay>(b =>
            {
                b.HasKey(x => x.Id);
            });

            builder.Entity<Subject>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<StudentSubject>(b =>
            {
                b.HasKey(ss => new { ss.StudentId, ss.SubjectId });

                b.HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<AttendanceSummary>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasOne(a => a.Student)
                .WithMany(s => s.AttendanceSummaries)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(a => a.Subject)
                .WithMany(s => s.AttendanceSummaries)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<StudentWeekDay>(b =>
            {
                b.HasKey(x=>new {x.StudentId,x.WeekDayId});

                b.HasOne(sw => sw.Student)
                .WithMany(s => s.StudentWeekDay)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(ew => ew.StudentId);

                b.HasOne(sw => sw.WeekDay)
                .WithMany(wd => wd.StudentWeekDay)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(ew => ew.WeekDayId);


                b.HasOne(sw => sw.Status)
                .WithMany(s => s.StudentWeekDays)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(ew => ew.StatusId);
            });
        }

        public DbSet<AttendanceSummary> AttendanceSummaries { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<StudentWeekDay> StudentWeekDays { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
    }
}
