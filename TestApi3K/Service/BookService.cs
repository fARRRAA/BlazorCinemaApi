using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace CinemaDigestApi.Service
{
    public class BookService : IBookService
    {
        readonly ContextDb _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string URL = "https://localhost:7270/api/Photos";
        private readonly HttpClient _httpClient;


        public BookService(ContextDb context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;

        }

        public List<Books> GetAllBooks([FromQuery] string? name, [FromQuery] string? genre, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            IQueryable<Books> query = _context.Books;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.title == name);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.genre == genre);
            }

            var totalBooks = query.Count();
            var books = new List<Books>();

            if (page.HasValue && pageSize.HasValue)
            {
                books = query.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return books;
            }

            return query.ToList();

        }
        public List<Books> GetBooksByName(string name, int? page, int? pageSize)
        {
            var nameParts = name.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var books = _context.Books.Where(i => nameParts.Any(part => i.title.ToLower().Contains(part))).ToList();
            if (page.HasValue && pageSize.HasValue)
            {
                books = books.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return books;
            }
            return books;
        }
        public async Task AddNewBook(CreateBook book)
        {
            var Book = new Books()
            {
                title = book.title,
                author = book.author,
                about = book.about,
                year = book.year,
                genre=book.genre,
                photo = book.photo,
            };
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBook(int id, CreateBook book)
        {
            Books? temp = await _context.Books.FirstOrDefaultAsync(b => b.id == id);
            temp.title = book.title;
            temp.author = book.author;
            temp.about = book.about;
            temp.year = book.year;
            temp.photo= book.photo;
            temp.genre = book.genre;
            await _context.SaveChangesAsync();

        }
        public async Task DeleteBook(int id)
        {
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.id == id);
            _context.Books.Remove(temp);
            _context.SaveChanges();
        }

        public List<Books> GetBooksByAuthor(string author)
        {
            var nameParts = author.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = _context.Books.Where(i => nameParts.All(part => i.author.ToLower().Contains(part))).ToList();
            return books;

        }


        public List<Books> GetBooksByGenre(string genre)
        {
            return _context.Books.Where(b => b.genre == genre).ToList();
        }



        public Books GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.id == id);
        }

        public bool BookExists(int id)
        {
            return (_context.Books.Any(b => b.id == id));

        }
        public List<Books> GetAll()
        {
            return _context.Books.ToList();
        }
        public async Task<string> UploadProfilePhoto(int bookId, IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync($"{URL}/upload", content);
                if (response.IsSuccessStatusCode)
                {
                    var photoURL = $"{URL}/photo/{file.FileName}";
                    var book = await _context.Books.FirstOrDefaultAsync(r => r.id == bookId);
                    book.photo = photoURL;
                    await _context.SaveChangesAsync();
                    return photoURL;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();

                    return $"{response.StatusCode}, {errorContent}";
                }

            }
        }

        public async Task<string> UpdateProfilePhoto(int bookId, IFormFile file)
        {
            var book = await _context.Books.FirstOrDefaultAsync(r => r.id == bookId);
            var fileName = removeUrl(book.photo);

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PutAsync($"{URL}/update/{fileName}", content);
                if (response.IsSuccessStatusCode)
                {
                    var photoURL = $"{URL}/photo/{file.FileName}";
                    book.photo = photoURL;
                    await _context.SaveChangesAsync();
                    return photoURL;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return $"{response.StatusCode}, {errorContent}";
                }
            }
        }

        public async Task DeleteProfilePhoto(int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(r => r.id == bookId);
            var fileName = removeUrl(book.photo);
            var response = await _httpClient.DeleteAsync($"{URL}/delete/{fileName}");
            if (response.IsSuccessStatusCode)
            {
                book.photo = "";
                await _context.SaveChangesAsync();
            }
        }

        public string removeUrl(string url)
        {
            var remove = "https://localhost:7270/api/Photos/photo/";
            if (url.StartsWith(remove))
            {
                return url.Substring(remove.Length);
            }
            return url.Substring(remove.Length);
        }

    }
}
