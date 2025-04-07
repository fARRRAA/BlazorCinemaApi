using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Interfaces
{
    public interface IBookService
    {
        public List<Books> GetAllBooks([FromQuery] string? name, [FromQuery] string? genre, [FromQuery] int? page, [FromQuery] int? pageSize);
        public Task AddNewBook(CreateBook book);
        public Task UpdateBook(int id, CreateBook book);
        public Task DeleteBook(int id);
        public List<Books> GetBooksByGenre(string genre);
        public List<Books> GetBooksByAuthor(string author);
        public List<Books> GetBooksByName(string name, int? page, int? pageSize);
        public Books GetBookById(int id);
        public bool BookExists(int id);
        public List<Books> GetAll();
        public Task<string> UploadProfilePhoto(int readerId, IFormFile photo);
        public Task<string> UpdateProfilePhoto(int readerId, IFormFile photo);
        public Task DeleteProfilePhoto(int readerId);
        public string removeUrl(string url);

    }
}
