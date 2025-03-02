using CinemaDigestApi.Model;

namespace CinemaDigestApi.Interfaces
{
    public interface IUserChatService
    {
        public UserChat GetUserChatByUsers(int senderId,int recipientId);
        public UserChat GetUserChat(int id);
    }
}
