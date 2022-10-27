namespace MokymoSistema.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Lecture> Lectures { get; set; }
}