using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Service
{
    public class UserChatMessagesService : IUserChatMessagesService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public UserChatMessagesService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public List<UserChatMesaage> GetUserChatMessages(int chatId)
        {
            return _context.UserChatMessages.Include(x => x.Chat).Include(x => x.User)
                .Include(x => x.Chat.FirstUser).Include(x => x.Chat.SecondUser)
                .Where(x => x.chatId == chatId).ToList();
        }
        public async Task AddUserMessage(UserChatMessageRequest message)
        {
            var newMessage = new UserChatMesaage()
            {
                message = message.message,
                chatId = message.chatId,
                userId = message.userId,
                sent_at = message.sent_at,
                photoUrl = message.photoUrl
            };
            await _context.UserChatMessages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateChatMessage(int id, UserChatMessageRequest mess)
        {
            var message = await _context.UserChatMessages.FirstOrDefaultAsync(x => x.id == id);
            if (message != null)
            {
                message.message = mess.message;
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserMessage(int id)
        {
            var mess = await _context.UserChatMessages.FirstOrDefaultAsync(x => x.id == id);
            _context.UserChatMessages.Remove(mess);
            await _context.SaveChangesAsync();
        }


    }
}
