using System.ComponentModel.DataAnnotations;

namespace API.Requests
{
    public class PositionSetRequest
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Range(1, 15, ErrorMessage = "Grade must be from 1 to 15")]
        public int? Grade { get; set; }
    }
}
