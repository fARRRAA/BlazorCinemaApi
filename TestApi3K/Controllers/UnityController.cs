using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class UnityController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;
        private string key = "secretkeyildarsecretkeyildarsecretkeyildar";

        public UnityController(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpGet("/api/unity/user/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var all = _context.UnityUsers.ToList();
            var user = await _context.UnityUsers.Include(x => x.role).FirstOrDefaultAsync(x => x.id == id);
            return Ok(user);
        }
        [HttpPost("/api/unity/user/{userId}/coins/add")]
        public async Task<IActionResult> AddCoins(int userId, [FromBody] Coin coin)
        {
            var user = await _context.UnityUsers.Include(x => x.role).FirstOrDefaultAsync(x => x.id == userId);
            user.coins += coin.coins;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("api/unity/user/{id}/coins")]
        public async Task<IActionResult> GetUserCoins(int id)
        {
            var user= await _context.UnityUsers.FirstOrDefaultAsync(x => x.id == id);
            var coins = new Coin
            {
                coins=user.coins
            };
            return Ok(coins);
        }
        [HttpPost("/api/unity/user/{userId}/ball/set")]
        public async Task<IActionResult> SetUserBall(int userId, [FromBody] Ball ball)
        {
            var user = await _context.UnityUsers.Include(x => x.role).FirstOrDefaultAsync(x => x.id == userId);
            user.ball = ball.ball;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/api/unity/user/{userId}/coins/reduce")]
        public async Task<IActionResult> ReduceCoins(int userId, [FromBody] Coin coin)
        {
            var user = await _context.UnityUsers.Include(x => x.role).FirstOrDefaultAsync(x => x.id == userId);
            user.coins -= coin.coins;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/api/unity/login")]
        public async Task<IActionResult> Login([FromBody] UserRes res)
        {
            var user = await _context.UnityUsers.Include(x => x.role).FirstOrDefaultAsync(x => x.login == res.login);
            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    error = "пользователь не найден"
                });
            }
            if (res.password != user.password)
            {
                return new NotFoundObjectResult(new
                {
                    error = "wrong password"
                });
            }
            var token = GenerateToken(user);
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("wild-cookies", token);
            Response.Headers.Add("Authorization", $"Bearer {token}");

            return new OkObjectResult(new
            {
                token = token,
                userId = user.id,
                role = user.role.name,
            });
        }

        private string GenerateToken(UnityUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Authentication, user.id.ToString()),
        new(ClaimTypes.Role, user.roleId switch { 1 => "admin", 2 => "user" })
    };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(6)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
