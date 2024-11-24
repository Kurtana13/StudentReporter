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
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.StudentSubjects)  // Include the subjects the student is enrolled in
                .ThenInclude(ss => ss.Subject)   // Include the subject details
                .ToListAsync();

            var studentViewModels = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Subjects = s.StudentSubjects.Select(ss => new SubjectViewModel
                {
                    Id = ss.Subject.Id,
                    Name = ss.Subject.Name
                }).ToList()
            }).ToList();

            return Ok(studentViewModels);
        }

        // Get student by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.StudentSubjects)
                .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Subjects = student.StudentSubjects.Select(ss => new SubjectViewModel
                {
                    Id = ss.Subject.Id,
                    Name = ss.Subject.Name
                }).ToList()
            };

            return Ok(studentViewModel);
        }

        // Create a new student
        [HttpPost]
        public async Task<ActionResult<StudentViewModel>> CreateStudent(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student
            {
                Name = studentViewModel.Name,
                Surname = studentViewModel.Surname
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Return the newly created student with their ID
            studentViewModel.Id = student.Id;

            return CreatedAtAction(nameof(GetStudent), new { id = studentViewModel.Id }, studentViewModel);
        }

        // Update an existing student
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.Id)
            {
                return BadRequest();
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentViewModel.Name;
            student.Surname = studentViewModel.Surname;

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a student
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
