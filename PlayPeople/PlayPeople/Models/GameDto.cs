using System.ComponentModel.DataAnnotations;

namespace PlayPeople.Models
{
    public class GameDto
    {
        [Required, MaxLength(100)]
        public string Title { get; set; } = "";

        [Required, MaxLength(100)]
        public string Developer { get; set; } = "";

        [Required, MaxLength(100)]
        public string Genre { get; set; } = "";

        public string? Description { get; set; } 

        public DateOnly ReleaseDate { get; set; }

        [Required, MaxLength(100)]
        public string Platforms { get; set; } = "";

        public  IFormFile? ImageFile { get; set; }
    }
}
