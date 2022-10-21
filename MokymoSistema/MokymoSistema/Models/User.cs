namespace MokymoSistema.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();
}