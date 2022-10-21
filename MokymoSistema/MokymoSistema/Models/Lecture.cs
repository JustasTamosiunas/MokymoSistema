namespace MokymoSistema.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public User Lecturer { get; set; }
        public string MaterialFileUrl { get; set; }
        public string AssignmentFileUrl { get; set; }
        public List<AssignmentUpload> AssignmentUploads { get; set; }
    }
}
