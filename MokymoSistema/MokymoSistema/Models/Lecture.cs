namespace MokymoSistema.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public string Assignment { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
