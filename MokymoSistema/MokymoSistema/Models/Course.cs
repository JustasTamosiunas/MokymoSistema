using System.ComponentModel.DataAnnotations;

namespace MokymoSistema.Models;

public class Course : IUserOwnedResouce
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Lecture> Lectures { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
}