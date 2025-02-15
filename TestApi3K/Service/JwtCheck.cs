using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaDigestApi.Service
{
    public class JwtCheck
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtCheck(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string IsUserAdmin()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["wild-cookies"];

            if (string.IsNullOrEmpty(cookieValue))
            {
                return "";
            }

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(cookieValue);
                var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Authentication)?.Value;
                var role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

                return role;
            }
            catch (Exception ex)
            {
                return $"Error reading token: {ex.Message}";
            }
        }
        public UserData GetUser(string token)
        {
            var cookieValue = token;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(cookieValue);
            var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Authentication)?.Value;
            var role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            return new UserData()
            {
                Id = Convert.ToInt32(userId),
                Role = role
            };
        }
    }
    public class UserData
    {
        public int Id { get; set; }
        public string Role { get; set; }
    }
}
