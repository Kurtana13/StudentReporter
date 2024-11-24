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
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherViewModel>>> GetTeachers()
        {
            var teachers = await _context.Teachers
                .Include(t => t.Subjects)  // Include Subjects that the teacher teaches
                .ToListAsync();

            var teacherViewModels = teachers.Select(t => new TeacherViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Subjects = t.Subjects.Select(s => new SubjectViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(teacherViewModels);
        }

        // Get teacher by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherViewModel = new TeacherViewModel
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Subjects = teacher.Subjects.Select(s => new SubjectViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };

            return Ok(teacherViewModel);
        }

        // Create a new teacher
        [HttpPost]
        public async Task<ActionResult<TeacherViewModel>> CreateTeacher(TeacherViewModel teacherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = new Teacher
            {
                Name = teacherViewModel.Name
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            teacherViewModel.Id = teacher.Id;

            return CreatedAtAction(nameof(GetTeacher), new { id = teacherViewModel.Id }, teacherViewModel);
        }

        // Update an existing teacher
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherViewModel teacherViewModel)
        {
            if (id != teacherViewModel.Id)
            {
                return BadRequest();
            }

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            teacher.Name = teacherViewModel.Name;

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a teacher
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
