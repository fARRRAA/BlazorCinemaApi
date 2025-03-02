using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDigestApi.Model
{
    public class UserChat
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(FirstUser))]
        public int firstUserId {  get; set; }
        public User FirstUser { get; set; }
        [ForeignKey(nameof(SecondUser))]
        public int secondUserId { get; set; }
        public User SecondUser { get; set; }
        public DateTime created_at { get; set; }
    }
}
