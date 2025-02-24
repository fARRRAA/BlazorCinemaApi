using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Service
{
    public class ChatService : IChatService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public ChatService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public MovieChat GetMovieChatById(int id)
        {
            return _context.MovieChats.Include(x=>x.Movie).FirstOrDefault(x => x.id == id);

        }

        public MovieChat GetMovieChatByMovieId(int id)
        {
            return _context.MovieChats.Include(x => x.Movie).FirstOrDefault(x => x.movieId == id);
        }

        public List<MovieChat> GetMovieChats()
        {
            return _context.MovieChats.Include(x => x.Movie).ToList();
        }
    }
}
