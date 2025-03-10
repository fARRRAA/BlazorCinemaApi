using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserChatMessageController : Controller
    {
        private readonly IUserChatMessagesService    _messages;

        public UserChatMessageController(IUserChatMessagesService messages)
        {
            _messages = messages;
        }
        [HttpGet("/userChat/messages/{id}")]
        public async Task<IActionResult> GetAllMesaages(int id)
        {
            return Ok(_messages.GetUserChatMessages(id));
        }
        [HttpPost("/userChat/messages/add")]
        public async Task<IActionResult> AddUserMessage([FromBody]UserChatMessageRequest message)
        {
            await _messages.AddUserMessage(message);
            return Ok();
        }
        [HttpPut("/userChat/messages/update/{id}")]
        public async Task<IActionResult> UpdateUserMessage(int id,[FromBody]UserChatMessageRequest messageReq)
        {
            await _messages.UpdateChatMessage(id, messageReq);
            return Ok();
        }
        [HttpDelete("/userChat/messages/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _messages.DeleteUserMessage(id);
            return Ok();
        }
    }
}
