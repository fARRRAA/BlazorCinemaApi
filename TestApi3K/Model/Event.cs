using System.ComponentModel.DataAnnotations;

namespace CinemaDigestApi.Model
{
    public class Event
    {
        [Key]
        public int id { get; set; } 
        public string name { get; set; }    
        public string type { get; set; }
        public DateTime date { get; set; }
        public string city { get; set; }
    }
    public class EventRequest
    {
        public string name { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
        public string city { get; set; }
    }

}
