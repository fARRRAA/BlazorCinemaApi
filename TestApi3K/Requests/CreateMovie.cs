using CinemaDigestApi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDigestApi.Requests
{
    public class CreateMovie
    {
        public string name { get; set; }
        public int genreId { get; set; }
        public Genre Genre { get; set; }
        public string description { get; set; }
        public decimal rating { get; set; }
        public string image { get; set; }
        public TimeSpan duration { get; set; }
        public int releaseYear { get; set; }
        [NotMapped]
        public string DurationString
        {
            get => duration.ToString(@"hh\:mm\:ss"); // Форматирует TimeSpan как строку
            set
            {
                if (TimeSpan.TryParse(value, out var parsedDuration))
                {
                    duration = parsedDuration;
                }
            }
        }
    }
}
