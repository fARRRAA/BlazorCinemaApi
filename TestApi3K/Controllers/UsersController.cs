using Microsoft.AspNetCore.Mvc;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using CinemaDigestApi.Model;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userLoginService)
        {
            _userService = userLoginService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewUser([FromBody] CreateNewUser newUser)
        {
            if (_userService.GetAllUsers().Any(x => x.email == newUser.email))
            {
                return new BadRequestObjectResult(new
                {
                    error = "такая почта уже есть"
                });
            }
            await _userService.CreateNewUserAsync(newUser);
            return new OkObjectResult(new
            {
                status = true
            });
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest req)
        {
            return await _userService.LoginAsync(req.login, req.password);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateNewUser newUser)
        {
            if (_userService.GetAllUsers().Any(x => x.email == newUser.email))
            {
                return new BadRequestObjectResult(new
                {
                    error = "такая почта уже есть"
                });
            }
            await _userService.RegisterAsync(newUser);
            return new OkObjectResult(new
            {
                status = true
            });

        }
        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            if (!_userService.UserExists(id))
            {
                return new NotFoundObjectResult(new { error = "not found this user" });
            }
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, CreateNewUser user)
        {
            if (!_userService.UserExists(id))
            {
                return new NotFoundObjectResult(new { error = "not found this user" });

            }
            await _userService.UpdateUserAsync(id, user);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!_userService.UserExists(id))
            {
                return new NotFoundObjectResult(new { error = "not found this user" });

            }
            await _userService.DeleteUser(id);
            return Ok();
        }
        [HttpPost("/user/change/{id}")]
        public async Task<IActionResult> ChangeData(int id, [FromBody] ChangeLP lp)
        {
            await _userService.ChangeData(id, lp);
            return Ok();
        }
    }
}
