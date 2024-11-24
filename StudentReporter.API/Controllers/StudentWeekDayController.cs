using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentReporter.API.Data;
using StudentReporter.API.Model;
using StudentReporter.API.ViewModels;

namespace StudentReporter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentWeekDayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentWeekDayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentWeekDay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentWeekDayViewModel>>> GetAllStudentWeekDays()
        {
            var studentWeekDays = await _context.StudentWeekDays
                .Include(swd => swd.Student)
                .Include(swd => swd.WeekDay)
                .Include(swd => swd.Status)
                .Select(swd => new StudentWeekDayViewModel
                {
                    StudentId = swd.StudentId,
                    WeekDayId = swd.WeekDayId,
                    StudentName = swd.Student.Name,
                    StudentSurname = swd.Student.Surname,
                    WeekNumber = swd.WeekDay.Week,
                    StatusId = swd.StatusId,
                    StatusName = swd.Status != null ? swd.Status.Name : null
                })
                .ToListAsync();

            return Ok(studentWeekDays);
        }

        // GET: api/StudentWeekDay/week/5
        [HttpGet("week/{weekNumber}")]
        public async Task<ActionResult<IEnumerable<StudentWeekDayViewModel>>> GetStudentWeekDaysByWeek(int weekNumber)
        {
            var studentWeekDays = await _context.StudentWeekDays
                .Include(swd => swd.Student)
                .Include(swd => swd.WeekDay)
                .Include(swd => swd.Status)
                .Where(swd => swd.WeekDay.Week == weekNumber)
                .Select(swd => new StudentWeekDayViewModel
                {
                    StudentId = swd.StudentId,
                    WeekDayId = swd.WeekDayId,
                    StudentName = swd.Student.Name,
                    StudentSurname = swd.Student.Surname,
                    WeekNumber = swd.WeekDay.Week,
                    StatusId = swd.StatusId,
                    StatusName = swd.Status != null ? swd.Status.Name : null
                })
                .ToListAsync();

            if (!studentWeekDays.Any())
            {
                return NotFound("No StudentWeekDay records found for the specified week.");
            }

            return Ok(studentWeekDays);
        }

        // PATCH: api/StudentWeekDay/attend/{studentId}
        [HttpPatch("attend/{studentId}/{weekDayId}/{subjectId}")]
        public async Task<IActionResult> PatchAttendanceStatus(int studentId, int weekDayId, int subjectId, [FromBody] StatusUpdateViewModel statusUpdate)
        {
            if (statusUpdate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the StudentWeekDay exists for the given student and weekday
            var studentWeekDay = await _context.StudentWeekDays
                .FirstOrDefaultAsync(swd => swd.StudentId == studentId && swd.WeekDayId == weekDayId);

            if (studentWeekDay == null)
            {
                return NotFound("StudentWeekDay not found.");
            }

            // Update the attendance status
            studentWeekDay.StatusId = statusUpdate.StatusId;

            // If the status is "Attended", update the AttendanceSummary
            if (statusUpdate.StatusId == 1) // Assuming 1 is "Attended"
            {
                var attendanceSummary = await _context.AttendanceSummaries
                    .FirstOrDefaultAsync(a => a.StudentId == studentId && a.SubjectId == subjectId);

                if (attendanceSummary == null)
                {
                    attendanceSummary = new AttendanceSummary
                    {
                        StudentId = studentId,
                        SubjectId = subjectId,
                        TotalAttendances = 1
                    };
                    _context.AttendanceSummaries.Add(attendanceSummary);
                }
                else
                {
                    attendanceSummary.TotalAttendances++;
                }

                await _context.SaveChangesAsync();
            }

            // Mark the studentWeekDay entity as modified
            _context.Entry(studentWeekDay).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
