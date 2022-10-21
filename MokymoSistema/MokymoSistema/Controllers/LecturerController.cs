using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MokymoSistema.Models;

namespace MokymoSistema.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LecturerController : Controller
    {
        private readonly DatabaseContext _context;

        public LecturerController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCourseMaterial(int lecturerId, int courseId, string lectureMaterialTEMP, string lectureAssignmentTEMP)
        {
            var course = await _context.Courses.Include(x=> x.Users).Include(x => x.Lectures).SingleOrDefaultAsync(x => x.Id == courseId);
            var lecturer = await _context.Users.SingleOrDefaultAsync(x => x.Id == lecturerId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (lecturer == null)
            {
                return BadRequest("Lecturer does not exist");
            }

            if (course.Users == null || course.Users.All(x => x.Id != lecturerId))
            {
                return BadRequest("Lecturer is not registered to this course");
            }

            var lecture = new Lecture
            {
                AssignmentFileUrl = lectureAssignmentTEMP, Lecturer = lecturer, MaterialFileUrl = lectureMaterialTEMP
            };

            _context.Lectures.Add(lecture);
            course.Lectures.Add(lecture);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLectureMaterial(int lecturerId, int courseId, int lectureId)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == courseId);
            var lecturer = await _context.Users.SingleOrDefaultAsync(x => x.Id == lecturerId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (lecturer == null)
            {
                return BadRequest("Lecturer does not exist");
            }

            if (course.Users.All(x => x.Id != lecturerId))
            {
                return Forbid("Lecturer is not registered to this course");
            }

            var lecture = await _context.Lectures.SingleOrDefaultAsync(x => x.Id == lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture does not exist");
            }

            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment(int lecturerId, int courseId, int lectureId, string AssignmentTEMP)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(x => x.Id == courseId);
            var lecturer = await _context.Users.SingleOrDefaultAsync(x => x.Id == lecturerId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (lecturer == null)
            {
                return BadRequest("Lecturer does not exist");
            }

            if (course.Users.All(x => x.Id != lecturerId))
            {
                return Forbid("Lecturer is not registered to this course");
            }

            var lecture = await _context.Lectures.SingleOrDefaultAsync(x => x.Id == lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture does not exist");
            }

            lecture.AssignmentFileUrl = AssignmentTEMP;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> GradeAssignment(int lecturerId, int courseId, int lectureId, int assignmentUploadId, double grade)
        {
            var course = await _context.Courses.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == courseId);
            var lecturer = await _context.Users.SingleOrDefaultAsync(x => x.Id == lecturerId);

            if (course == null)
            {
                return NotFound("Course does not exist");
            }

            if (lecturer == null)
            {
                return BadRequest("Lecturer does not exist");
            }

            if (course.Users.All(x => x.Id != lecturerId))
            {
                return Forbid("Lecturer is not registered to this course");
            }

            var lecture = await _context.Lectures.Include(x => x.AssignmentUploads).SingleOrDefaultAsync(x => x.Id == lectureId);

            if (lecture == null)
            {
                return NotFound("Lecture does not exist");
            }

            var assignmentUpload = lecture.AssignmentUploads.SingleOrDefault(x => x.Id == assignmentUploadId);
            if (assignmentUpload == null)
            {
                return NotFound("Assignment Upload does not exist");
            }

            assignmentUpload.Grade = new Grade
            {
                Result = grade,
                Student = assignmentUpload.Student
            };

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
