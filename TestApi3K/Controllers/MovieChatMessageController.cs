using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieChatMessageController : Controller
    {
        private readonly IMovieChatMessages _messages;

        public MovieChatMessageController(IMovieChatMessages messages)
        {
            _messages = messages;
        }
        [HttpGet("api/movieChat/movie/messages")]
        public IActionResult GetAllMessages()
        {
            return Ok( _messages.GetMovieChatMessages());    
        }
        [HttpPost("api/movieChat/movie/message/add")]
        public async Task<IActionResult> AddMovieChatMessage([FromBody]MovieChatMessageRequest moviereq)
        {
            await _messages.AddMovieMessage(moviereq);
            return Ok();
        }

    }
}
