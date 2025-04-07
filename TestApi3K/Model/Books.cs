using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class Books
    {
        [Key]
        public int id { get; set; }

        public string author { get; set; }

        public string genre { get; set; }

        public string title { get; set; }
        public string about { get; set; }
        public DateTime year { get; set; }
        public string photo { get; set; }
    }
}
