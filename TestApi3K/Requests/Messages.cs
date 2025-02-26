using CinemaDigestApi.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Requests
{
    public class UserChatMessageRequest
    {
        public int chatId { get; set; }
        public UserChat Chat { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public string message { get; set; }
        public DateTime sent_at { get; set; }
        public string photoUrl { get; set; }
    }
    public class MovieChatMessageRequest
    {
        public int chatId { get; set; }
        public MovieChat Chat { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public string message { get; set; }
        public DateTime sent_at { get; set; }
        public string photoUrl { get; set; }
    }
}
