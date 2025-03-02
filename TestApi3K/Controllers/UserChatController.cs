using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserChatController : Controller
    {
        private IUserChatService _chatService;
        public UserChatController(IUserChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("chat/{id}")]
        public async Task<IActionResult> GetUserChat(int id)
        {
            return Ok(_chatService.GetUserChat(id));
        }
        [HttpGet("chat/from/{senderId}/to/{recipientId}")]
        public async Task<IActionResult> GetUserChatByUsers(int senderId, int recipientId)
        {
            return Ok(_chatService.GetUserChatByUsers(senderId, recipientId));
        }
    }
}
