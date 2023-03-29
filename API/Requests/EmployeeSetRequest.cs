using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace API.Requests
{
    public class EmployeeSetRequest
    {
        [Required]
        public int Id { get; set; }

        public string? FIO { get; set; }

        public DateTime? Birthday { get; set; }

        public List<int>? Positions { get; set; }
    }
}
