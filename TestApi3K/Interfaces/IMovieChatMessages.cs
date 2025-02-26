using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;

namespace CinemaDigestApi.Interfaces
{
    public interface IMovieChatMessages
    {
        public List<MovieChatMessage> GetMovieChatMessages();
        public Task AddMovieMessage(MovieChatMessageRequest message);
        public Task DeleteMovieMessage(int id);
    }
}
