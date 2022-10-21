using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MokymoSistema.Models;

namespace MokymoSistema.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : Controller
    {
        private readonly DatabaseContext _context;

        public StudentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(string firstName, string lastName)
        {
            var lecturer = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Role = Role.Student,
            };

            _context.Users.Add(lecturer);
            await _context.SaveChangesAsync();

            return Ok(lecturer);
        }

        [HttpGet(Name = "GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(int courseId, int studentId)
        {
            var course = await _context.Courses.Include(x => x.Lectures).Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (!course.Users.Any(x => x.Id == studentId))
            {
                return Forbid("Student is not registered to this course");
            }

            return Ok(course);
        }

        [HttpPost(Name = "RegisterToCourse")]
        public async Task<IActionResult> RegisterToCourse(int studentId, int courseId)
        {
            var course = await _context.Courses.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == courseId);
            var student = await _context.Users.SingleOrDefaultAsync(x => x.Id == studentId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (student == null)
            {
                return BadRequest("Student does not exist");
            }

            course.Users.Add(student);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet(Name = "DownloadMaterial")]
        public async Task<IActionResult> DownloadLectureMaterial(int courseId, int studentId, int lectureId)
        {
            var course = await _context.Courses.Include(x=>x.Users).Include(x => x.Lectures).SingleOrDefaultAsync(x => x.Id == courseId);
            var student = await _context.Users.SingleOrDefaultAsync(x => x.Id == studentId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (student == null)
            {
                return BadRequest("Student does not exist");
            }

            if (course.Users.All(x => x.Id != studentId))
            {
                return BadRequest("Student is not registered to this course");
            }

            var lecture = await _context.Lectures.SingleOrDefaultAsync(x => x.Id == lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture does not exist");
            }

            return Ok(lecture.MaterialFileUrl);
        }

        [HttpPost(Name = "UploadAssignment")]
        public async Task<IActionResult> UploadAssigment(int courseId, int studentId, int lectureId,
            string assignmentUploadTEMP)
        {
            var course = await _context.Courses.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == courseId);
            var student = await _context.Users.SingleOrDefaultAsync(x => x.Id == studentId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (student == null)
            {
                return BadRequest("Student does not exist");
            }

            if (course.Users.All(x => x.Id != studentId))
            {
                return Forbid("Student is not registered to this course");
            }

            var lecture = await _context.Lectures.Include(x => x.AssignmentUploads).SingleOrDefaultAsync(x => x.Id == lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture does not exist");
            }

            var assignmentUpload = new AssignmentUpload { Student = student, UploadUrl = assignmentUploadTEMP };
            lecture.AssignmentUploads.Add(assignmentUpload);
            await _context.SaveChangesAsync();
            return Ok(assignmentUpload);
        }
    }
}
