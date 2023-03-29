using System.ComponentModel.DataAnnotations;

namespace API.Requests
{
    public class EmployeeCreateRequest
    {
        [Required]
        public string? FIO { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        public List<int> Positions { get; set; } = new();
    }
}
