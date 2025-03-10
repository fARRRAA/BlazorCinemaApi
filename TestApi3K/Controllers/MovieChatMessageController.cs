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
        [HttpGet("api/movieChat/movie/messages/{chatId}")]
        public IActionResult GetAllMessages(int chatId)
        {
            return Ok( _messages.GetMovieChatMessages(chatId));    
        }
        [HttpPost("api/movieChat/movie/message/add")]
        public async Task<IActionResult> AddMovieChatMessage([FromBody]MovieChatMessageRequest moviereq)
        {
            await _messages.AddMovieMessage(moviereq);
            return Ok();
        }
        [HttpPut("/api/movieChat/movie/message/edit/{id}")]
        public async Task<IActionResult> EditMovieMessage(int id, [FromBody]MovieChatMessageRequest moviereq)
        {
            await _messages.UpdateMovieMessage(id, moviereq);
            return Ok();
        }
        [HttpDelete("/api/movieChat/movie/message/delete/{id}")]
        public async Task<IActionResult> DeleteMovieMessage(int id)
        {
            await _messages.DeleteMovieMessage(id);
            return Ok();
        }
    }
}
