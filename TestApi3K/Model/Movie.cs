using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDigestApi.Model
{
    public class Movie
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        [ForeignKey(nameof(Genre))]
        public int genreId { get; set; }
        public Genre Genre { get; set; }
        public string description { get; set; }
        public decimal rating { get; set; }
        public string image { get; set; }
        public TimeSpan duration { get; set; }
        public int releaseYear { get; set; }
    }
}
