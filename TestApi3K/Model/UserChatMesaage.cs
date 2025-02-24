using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class UserChatMesaage
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(Chat))]
        public int chatId { get; set; }
        public UserChat Chat { get; set; }
        [ForeignKey(nameof(User))]
        public int userId { get; set; }
        public User User { get; set; }
        public string message { get; set; }
        public DateTime sent_at { get; set; }
        public string photoUrl { get; set; }
    }
}
