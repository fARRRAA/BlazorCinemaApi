using CinemaDigestApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        public readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("chat/{id}")]
        public async Task<IActionResult> GetChatById(int id)
        {
            return Ok(_chatService.GetMovieChatById(id));

        }
        [HttpGet("chat/movie/{id}")]
        public async Task<IActionResult> GetChatByMovieId(int id)
        {
            return Ok(_chatService.GetMovieChatByMovieId(id));

        }
    }
}
