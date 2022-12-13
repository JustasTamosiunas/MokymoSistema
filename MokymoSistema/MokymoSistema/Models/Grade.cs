using System.ComponentModel.DataAnnotations;

namespace MokymoSistema.Models;

public class Grade : IUserOwnedResouce
{
    public int Id { get; set; }
    public double Result { get; set; }
    public string StudentName { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
}