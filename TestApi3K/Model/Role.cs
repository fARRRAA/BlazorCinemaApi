using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
