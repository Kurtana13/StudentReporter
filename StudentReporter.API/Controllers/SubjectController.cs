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
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectViewModel>>> GetSubjects()
        {
            var subjects = await _context.Subjects
                                          .Include(s => s.Teacher)
                                          .Include(s => s.StudentSubjects)
                                              .ThenInclude(ss => ss.Student)
                                          .ToListAsync();

            var subjectViewModels = subjects.Select(s => new SubjectViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TeacherName = s.Teacher != null ? s.Teacher.Name : "No Teacher Assigned",
                Students = s.StudentSubjects.Select(ss => new StudentViewModel
                {
                    Id = ss.Student.Id,
                    Name = ss.Student.Name,
                    Surname = ss.Student.Surname
                }).ToList()
            }).ToList();

            return Ok(subjectViewModels);
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectViewModel>> GetSubject(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Student)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            var subjectViewModel = new SubjectViewModel
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherName = subject.Teacher?.Name,
                Students = subject.StudentSubjects.Select(ss => new StudentViewModel
                {
                    Id = ss.StudentId,
                    Name = ss.Student.Name,
                    Surname = ss.Student.Surname
                }).ToList()
            };

            return Ok(subjectViewModel);
        }

        // POST: api/Subject
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<SubjectViewModel>> CreateSubject(SubjectViewModel subjectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = new Subject
            {
                Name = subjectViewModel.Name,
                TeacherId = subjectViewModel.TeacherName != null ? _context.Teachers
                    .FirstOrDefault(t => t.Name == subjectViewModel.TeacherName)?.Id : (int?)null
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            subjectViewModel.Id = subject.Id;

            return CreatedAtAction(nameof(GetSubject), new { id = subjectViewModel.Id }, subjectViewModel);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectViewModel subjectViewModel)
        {
            if (id != subjectViewModel.Id)
            {
                return BadRequest();
            }

            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            subject.Name = subjectViewModel.Name;
            subject.TeacherId = subjectViewModel.TeacherName != null ? _context.Teachers
                .FirstOrDefault(t => t.Name == subjectViewModel.TeacherName)?.Id : (int?)null;

            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}