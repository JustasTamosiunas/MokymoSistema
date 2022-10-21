using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MokymoSistema.Models;

namespace MokymoSistema.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController : Controller
{
    private readonly DatabaseContext _context;

    public AdminController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLecturer(string firstName, string lastName)
    {
        var lecturer = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Role = Role.Lecturer,
        };

        _context.Users.Add(lecturer);
        await _context.SaveChangesAsync();

        return Ok(lecturer);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLecturer(int id)
    {
        var lecturer = await _context.Users.FindAsync(id);
        if (lecturer == null)
        {
            return NotFound("Lecturer does not exist");
        }

        if (lecturer.Role != Role.Lecturer)
        {
            return BadRequest("User is not a lecturer");
        }
        _context.Users.Remove(lecturer);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(string name, int lecturerId)
    {
        var lecturer = await _context.Users.FindAsync(lecturerId);
        if (lecturer == null)
        {
            return NotFound("Lecturer does not exist");
        }

        var course = new Course
        {
            Name = name,
            Users = new List<User> { lecturer }
        };

        lecturer.Courses.Add(course);
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return Ok(course);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound("Course doesn't exist");
        }

        _context.Courses.Remove(course);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveStudentFromCourse(int studentId, int courseId)
    {
        var student = await _context.Users.FindAsync(studentId);
        var course = await _context.Courses.FindAsync(courseId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        if (course == null)
        {
            return NotFound("Course not found");
        }

        if (student.Role != Role.Student)
        {
            return BadRequest("User is not a student");
        }

        course.Users.RemoveAll(x => x.Id == studentId);
        student.Courses.RemoveAll(x => x.Id == courseId);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentGrades(int studentId)
    {
        var student = await _context.Users.FindAsync(studentId);
        if (student == null)
        {
            return NotFound("Student not found");
        }

        return Ok(_context.AssignmentUploads.Include(x => x.Student).Include(x => x.Grade).Where(x => x.Student == student).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        return Ok(_context.Users.Include(x => x.Courses).Where(x => x.Role == Role.Student).ToList());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLecturers()
    {
        return Ok(_context.Users.Include(x => x.Courses).Where(x => x.Role == Role.Lecturer).ToList());
    }
}