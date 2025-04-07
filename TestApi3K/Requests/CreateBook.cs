namespace CinemaDigestApi.Requests
{
    public class CreateBook
    {
        public string author { get; set; }
        public string genre { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public DateTime year { get; set; }
        public string photo { get; set; }
    }
}
