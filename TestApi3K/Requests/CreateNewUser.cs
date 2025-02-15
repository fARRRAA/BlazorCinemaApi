namespace CinemaDigestApi.Requests
{
    public class CreateNewUser
    {
        public string name { get; set; }
        public string description { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }
    public class LoginRequest
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}
