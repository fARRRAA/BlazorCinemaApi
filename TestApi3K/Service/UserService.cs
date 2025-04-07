using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaDigestApi.Service
{
    public class UserService : IUsersService
    {
        private readonly ContextDb _context;
        private string key = "secretkeyildarsecretkeyildarsecretkeyildar";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return new OkObjectResult(new
            {
                data = new { users = users },
                status = true
            });
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = _context.Users.Include(x=>x.role).FirstOrDefault(x => x.id == id);
            return user;
        }
        public async Task CreateNewUserAsync(CreateNewUser newUser)
        {
            var user = new User()
            {
                name = newUser.name,
                description = newUser.description,
                login = newUser.login,
                password = newUser.password,
                email = newUser.email,
                roleId = 2
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> LoginAsync(string login, string password)
        {
            var user = await _context.Users.Include(x => x.role).FirstOrDefaultAsync(x => x.login == login);
            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    error = "пользователь не найден"
                });
            }
            if (password != user.password)
            {
                return new NotFoundObjectResult(new
                {
                    error = "wrong password"
                });
            }
            var token = GenerateToken(user);
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("wild-cookies", token);
            //Response.Headers.Add("Authorization", $"Bearer {token}");

            return new OkObjectResult(new
            {
                token = token,
                userId = user.id,
                role = user.role.name,
            });
        }

        public async Task RegisterAsync(CreateNewUser newUser)
        {
            var user = new User()
            {
                name = newUser.name,
                description = newUser.description,
                login = newUser.login,
                password = newUser.password,
                email = newUser.email,
                roleId = 2
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public string GenerateToken(User user)
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

        public bool UserExists(int id)
        {
            return _context.Users.Any(x => x.id == id);
        }

        public async Task UpdateUserAsync(int id,CreateNewUser newUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            user.email = newUser.email;
            user.password = newUser.password;
            user.name = newUser.name;
            user.login = newUser.login;
            user.description = newUser.description;
            await _context.SaveChangesAsync();

        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task ChangeData(int id,ChangeLP lp)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            if (user == null) return;
            user.login =    lp.login;
            user.password = lp.password;
            await _context.SaveChangesAsync();
        }
    }
}
