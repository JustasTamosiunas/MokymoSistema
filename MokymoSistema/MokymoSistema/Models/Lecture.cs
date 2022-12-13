using System.ComponentModel.DataAnnotations;

namespace MokymoSistema.Models
{
    public class Lecture : IUserOwnedResouce
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public string Assignment { get; set; }
        public List<Grade> Grades { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
