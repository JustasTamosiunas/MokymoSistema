namespace MokymoSistema.Models;

public class AssignmentUpload
{
    public int Id { get; set; }
    public string? UploadUrl { get; set; }
    public User? Student { get; set; }
    public Grade? Grade { get; set; }
}