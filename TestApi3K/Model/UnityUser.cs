using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class UnityUser
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public float coins { get; set; }
    }
}
