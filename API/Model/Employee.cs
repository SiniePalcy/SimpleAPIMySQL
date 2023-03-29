using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public DateTime Birthday { get; set; }

        public virtual ICollection<Position> Positions { get; set; } = new HashSet<Position>();
    }
}
