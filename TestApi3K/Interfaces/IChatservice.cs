using CinemaDigestApi.Model;

namespace CinemaDigestApi.Interfaces
{
    public interface IChatService
    {
        public List<MovieChat> GetMovieChats();
        public MovieChat GetMovieChatById(int id);
        public MovieChat GetMovieChatByMovieId(int id);

    }
}
