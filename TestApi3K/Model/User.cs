using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CinemaDigestApi.Model
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        [ForeignKey(nameof(role))]
        public int roleId { get; set; }
        public Role? role { get; set; }

    }
    public class ChangeLP
    {
        public string login { get; set; }
        public string password { get; set; }
    }
    public class UserRes
    {
       public string login { get; set; }
       public string password { get; set; }
    }
}
