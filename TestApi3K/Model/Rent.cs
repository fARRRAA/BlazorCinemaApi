using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class Rent
    {
        [Key]
        public int id { get; set; }
        public DateTime rentalStart { get; set; }
        public int rentalTime { get; set; }
        [ForeignKey(nameof(user))]
        public int userId { get; set; }
        public User user { get; set; }

        [ForeignKey(nameof(book))]
        public int bookId { get; set; }
        public Books book { get; set; }
        public DateTime rentalEnd { get; set; }
        public string status { get; set; }

    }
}
