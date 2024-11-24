using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentReporter.API.Data;
using StudentReporter.API.Model;
using StudentReporter.API.ViewModels;

namespace StudentReporter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekDayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeekDayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WeekDay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeekDayViewModel>>> GetWeekDays()
        {
            var weekDays = await _context.WeekDays
                .Select(wd => new WeekDayViewModel
                {
                    Id = wd.Id,
                    WeekNumber = wd.Week
                })
                .ToListAsync();

            return Ok(weekDays);
        }

        // GET: api/WeekDay/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WeekDayViewModel>> GetWeekDay(int id)
        {
            var weekDay = await _context.WeekDays.FindAsync(id);

            if (weekDay == null)
            {
                return NotFound("WeekDay not found.");
            }

            var viewModel = new WeekDayViewModel
            {
                Id = weekDay.Id,
                WeekNumber = weekDay.Week
            };

            return Ok(viewModel);
        }

        // POST: api/WeekDay
        [HttpPost]
        public async Task<IActionResult> CreateWeekDay([FromBody] WeekDayViewModel weekDayViewModel)
        {
            if (weekDayViewModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the week already exists
            if (await _context.WeekDays.AnyAsync(w => w.Week == weekDayViewModel.WeekNumber))
            {
                return Conflict("This week already exists.");
            }

            // Create the WeekDay entity
            var weekDay = new WeekDay
            {
                Week = weekDayViewModel.WeekNumber
            };

            _context.WeekDays.Add(weekDay);
            await _context.SaveChangesAsync(); // Save to get the WeekDay.Id

            // Automatically create StudentWeekDay records
            var studentWeekDays = await _context.Students
                .Select(student => new StudentWeekDay
                {
                    StudentId = student.Id,
                    WeekDayId = weekDay.Id,
                    StatusId = null // Default to no attendance status
                })
                .ToListAsync();

            _context.StudentWeekDays.AddRange(studentWeekDays);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeekDay), new { id = weekDay.Id }, weekDay);
        }

        // PUT: api/WeekDay/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeekDay(int id, [FromBody] WeekDayViewModel weekDayViewModel)
        {
            if (weekDayViewModel == null || id != weekDayViewModel.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingWeekDay = await _context.WeekDays.FindAsync(id);

            if (existingWeekDay == null)
            {
                return NotFound("WeekDay not found.");
            }

            existingWeekDay.Week = weekDayViewModel.WeekNumber;

            _context.Entry(existingWeekDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.WeekDays.Any(w => w.Id == id))
                {
                    return NotFound("WeekDay not found.");
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/WeekDay/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeekDay(int id)
        {
            var weekDay = await _context.WeekDays.FindAsync(id);

            if (weekDay == null)
            {
                return NotFound("WeekDay not found.");
            }

            var relatedStudentWeekDays = _context.StudentWeekDays.Where(swd => swd.WeekDayId == id);
            _context.StudentWeekDays.RemoveRange(relatedStudentWeekDays);

            _context.WeekDays.Remove(weekDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
