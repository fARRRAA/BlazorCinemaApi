using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;

namespace CinemaDigestApi.Interfaces
{
    public interface IUserChatMessagesService
    {
        public List<UserChatMesaage> GetUserChatMessages(int chatId);
        public Task AddUserMessage(UserChatMessageRequest message);
        public Task DeleteUserMessage(int id);
    }
}
