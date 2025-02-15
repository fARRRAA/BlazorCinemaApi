using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class Genre
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
