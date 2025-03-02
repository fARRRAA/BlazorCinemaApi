using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Service
{
    public class MovieChatMessagesService : IMovieChatMessages
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public MovieChatMessagesService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task AddMovieMessage(MovieChatMessageRequest message)
        {
            var newMessage = new MovieChatMessage()
            {
                message=message.message,
                chatId=message.chatId,
                userId=message.userId,
                sent_at=message.sent_at,
                photoUrl=message.photoUrl
            };
            await _context.MovieChatMessages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
           
        }

        public async Task DeleteMovieMessage(int id)
        {
           var mess = await _context.MovieChatMessages.FirstOrDefaultAsync(x=>x.id==id);
            _context.MovieChatMessages.Remove(mess);
            await _context.SaveChangesAsync();

        }

        public List<MovieChatMessage> GetMovieChatMessages(int chatId)
        {
            return _context.MovieChatMessages.Include(x=>x.Chat).Include(x=>x.User).Include(x=>x.Chat.Movie)
                .Where(x=>x.chatId==chatId).ToList();
        }
    }
}
