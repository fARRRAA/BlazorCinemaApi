using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Service
{
    public class UserChatService : IUserChatService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public UserChatService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public UserChat GetUserChat(int id)
        {
            var chat = _context.UserChats.Include(x=>x.FirstUser).Include(x=>x.SecondUser).FirstOrDefault(x => x.id == id);
            return chat;
        }

        public UserChat GetUserChatByUsers(int senderId, int recipientId)
        {
            var chat = _context.UserChats.Include(x => x.FirstUser).Include(x => x.SecondUser)
                .FirstOrDefault(x=>(x.firstUserId == senderId&&x.secondUserId==recipientId)|(x.secondUserId == senderId && x.firstUserId == recipientId));

            if (chat == null)
            {
                var chatAdd = new UserChat()
                {
                    firstUserId = senderId,
                    secondUserId = recipientId,
                    created_at= DateTime.Now,
                };
                _context.UserChats.Add(chatAdd);
                _context.SaveChanges();
                var check = _context.UserChats.Include(x => x.FirstUser).Include(x=>x.FirstUser.role).Include(x => x.SecondUser.role).Include(x => x.SecondUser)
                    .FirstOrDefault(x => x.firstUserId == chatAdd.firstUserId && x.secondUserId == chatAdd.secondUserId);
                return check;
            }
            return chat;

        }
    }
}
