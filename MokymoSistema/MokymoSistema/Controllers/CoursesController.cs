using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using MokymoSistema.DTO;
using MokymoSistema.Models;

namespace MokymoSistema.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private DatabaseContext _databaseContext;
        private readonly IAuthorizationService _authorizationService;

        public CoursesController(DatabaseContext databaseContext, IAuthorizationService authorizationService)
        {
            _databaseContext = databaseContext;
            _authorizationService = authorizationService;
        }

        [Route("courses")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty");
            }

            var course = new Course
            {
                Name = request.Name,
                Lectures = new List<Lecture>(),
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            _databaseContext.Add(course);
            await _databaseContext.SaveChangesAsync();
            return Created("/courses", course);
        }

        [Route("courses")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _databaseContext.Courses.ToListAsync();
            return Ok(courses);
        }

        [Route("courses/{courseId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourse([FromRoute] int courseId)
        {
            var course = await _databaseContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            return Ok(course);
        }

        [Route("courses/{courseId}")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> EditCourse([FromRoute] int courseId, CreateCourseDTO request)
        {
            var course = await _databaseContext.Courses.FindAsync(courseId);

            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            course.Name = request.Name;
            await _databaseContext.SaveChangesAsync();

            return Ok(course);
        }

        [Route("courses/{courseId}")]
        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            var course = await _databaseContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, course, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _databaseContext.Courses.Remove(course);
            await _databaseContext.SaveChangesAsync();

            return NoContent();
        }

        [Route("courses/{courseId}/lectures")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLectures([FromRoute] int courseId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            return Ok(course.Lectures);
        }

        [Route("courses/{courseId}/lectures/{lectureId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLecture([FromRoute] int courseId, [FromRoute] int lectureId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            return Ok(lecture);
        }

        [Route("courses/{courseId}/lectures")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> CreateLecture([FromRoute] int courseId, CreateLectureDTO request)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            if (string.IsNullOrEmpty(request.Material))
            {
                return BadRequest("Lecture material cannot be empty");
            }

            var lecture = new Lecture
            {
                Material = request.Material,
                Assignment = request.Assignment,
                Grades = new List<Grade>(),
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            course.Lectures.Add(lecture);

            _databaseContext.Add(lecture);
            await _databaseContext.SaveChangesAsync();
            return Created("/lectures", lecture);
        }

        [Route("courses/{courseId}/lectures/{lectureId}")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> EditLecture([FromRoute] int courseId, [FromRoute] int lectureId, CreateLectureDTO request)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            lecture = await _databaseContext.Lectures.FindAsync(lectureId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, lecture, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(request.Material))
            {
                lecture.Material = request.Material;
            }

            if (!string.IsNullOrEmpty(request.Assignment))
            {
                lecture.Assignment = request.Assignment;
            }

            await _databaseContext.SaveChangesAsync();

            return Ok(lecture);
        }

        [Route("courses/{courseId}/lectures/{lectureId}")]
        [HttpDelete]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteLecture([FromRoute] int courseId, [FromRoute] int lectureId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, lecture, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _databaseContext.Lectures.Remove(lecture);
            await _databaseContext.SaveChangesAsync();

            return NoContent();
        }

        [Route("courses/{courseId}/lectures/{lectureId}/grades")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGrades([FromRoute] int courseId, [FromRoute] int lectureId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            return Ok(lecture.Grades);
        }

        [Route("courses/{courseId}/lectures/{lectureId}/grades/{gradeId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGrade([FromRoute] int courseId, [FromRoute] int lectureId, [FromRoute] int gradeId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            var grade = lecture.Grades.FirstOrDefault(x => x.Id == gradeId);
            if (grade == null)
            {
                return NotFound("Grade with this ID does not exist in this lecture.");
            }

            return Ok(grade);
        }

        [Route("courses/{courseId}/lectures/{lectureId}/grades")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> CreateGrade([FromRoute] int courseId, [FromRoute] int lectureId, GradeDTO request)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            if (string.IsNullOrEmpty(request.StudentName))
            {
                return BadRequest("Please provide the students name");
            }

            if (request.Grade is < 0 or > 10)
            {
                return BadRequest(new { Error = "Grades have to be in the range 0-10"});
            }

            var grade = new Grade
            {
                Result = request.Grade,
                StudentName = request.StudentName,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            lecture.Grades.Add(grade);

            _databaseContext.Add(grade);
            await _databaseContext.SaveChangesAsync();
            return Created("/grades", grade);
        }

        [Route("courses/{courseId}/lectures/{lectureId}/grades/{gradeId}")]
        [HttpPut]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> EditGrade([FromRoute] int courseId, [FromRoute] int lectureId, [FromRoute] int gradeId, GradeDTO request)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            var grade = lecture.Grades.FirstOrDefault(x => x.Id == gradeId);
            if (grade == null)
            {
                return NotFound("Grade with this ID does not exist in this lecture.");
            }

            if (request.Grade is < 0 or > 10)
            {
                return BadRequest("Grades have to be in the range 0-10");
            }

            grade = await _databaseContext.Grades.FindAsync(gradeId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, grade, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(request.StudentName))
            {
                grade.StudentName = request.StudentName;
            }

            if (request.Grade != 0)
            {
                grade.Result = request.Grade;
            }

            await _databaseContext.SaveChangesAsync();

            return Ok(grade);
        }

        [Route("courses/{courseId}/lectures/{lectureId}/grades/{gradeId}")]
        [HttpDelete]
        [Authorize(Roles = UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteGrade([FromRoute] int courseId, [FromRoute] int lectureId, [FromRoute] int gradeId)
        {
            var course = await _databaseContext.Courses.Include(x => x.Lectures).ThenInclude(x => x.Grades).FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course with this ID doesn't exist");
            }

            var lecture = course.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (lecture == null)
            {
                return NotFound("Lecture with this ID does not exist in this course.");
            }

            var grade = lecture.Grades.FirstOrDefault(x => x.Id == gradeId);
            if (grade == null)
            {
                return NotFound("Grade with this ID does not exist in this lecture.");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, grade, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _databaseContext.Grades.Remove(grade);
            await _databaseContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
