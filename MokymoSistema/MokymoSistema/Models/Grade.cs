namespace MokymoSistema.Models;

public class Grade
{
    public int Id { get; set; }
    public double Result { get; set; }
    public User Student { get; set; }
}