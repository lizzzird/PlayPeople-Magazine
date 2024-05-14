using System.ComponentModel.DataAnnotations;

namespace PlayPeople.Models
{
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; } = "";

        [MaxLength(100)]
        public string Developer { get; set; } = "";

        [MaxLength(100)]
        public string Genre { get; set; } = "";

        public string Description { get; set; } = "";

        public DateOnly ReleaseDate { get; set; }

        [MaxLength(100)]
        public string Platforms { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

    }
}
