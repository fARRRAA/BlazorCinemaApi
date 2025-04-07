using Microsoft.AspNetCore.Mvc;
using CinemaDigestApi.Requests;
using CinemaDigestApi.Model;

namespace CinemaDigestApi.Interfaces
{
    public interface IUsersService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task CreateNewUserAsync(CreateNewUser newUser);
        Task<IActionResult> LoginAsync(string login, string password);
        Task RegisterAsync(CreateNewUser user);
        Task<User> GetUserByIdAsync(int id);
        bool UserExists(int id);
        Task UpdateUserAsync(int id,CreateNewUser newUser);
        Task DeleteUser(int id);
        Task ChangeData(int id,ChangeLP lp);
        List<User> GetAllUsers();
    }
}
