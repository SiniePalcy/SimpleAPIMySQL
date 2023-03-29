using System.ComponentModel.DataAnnotations;

namespace API.Requests
{
    public class PositionCreateRequest
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(1, 15, ErrorMessage = "Grade must be from 1 to 15")]
        public int? Grade { get; set; }
    }
}
