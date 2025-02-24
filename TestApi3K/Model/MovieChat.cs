using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDigestApi.Model
{
    public class MovieChat
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(Movie))]
        public int movieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime created_at { get; set; }

    }
}
