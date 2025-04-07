using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CinemaDigestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly IBookService _books;

        public BooksController(IBookService bookService, IGenreService genreService)
        {
            _books = bookService;
        }
        [HttpGet]
        [Route("allBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = _books.GetAll();
            return Ok(books);
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? name, [FromQuery] string? genre,  [FromQuery] int? page,
        [FromQuery] int? pageSize)
        {
            var books = _books.GetAllBooks(name, genre, page, pageSize);
            return Ok(books);
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewBook([FromBody] CreateBook book)
        {

            if (string.IsNullOrWhiteSpace(book.title) || string.IsNullOrWhiteSpace(book.author) || string.IsNullOrWhiteSpace(book.about)  || string.IsNullOrWhiteSpace(book.about) || string.IsNullOrWhiteSpace(Convert.ToString(book.year)))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }

            if (_books.GetAll().Any(b => b.author == book.author && b.title == book.title))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("this book is already exists")
                });
            }
            await _books.AddNewBook(book);
            return Ok();
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBook book)
        {

            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            if (string.IsNullOrWhiteSpace(book.title) || string.IsNullOrWhiteSpace(book.author)  || string.IsNullOrWhiteSpace(book.about) || string.IsNullOrWhiteSpace(Convert.ToString(book.year)))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });

            }
            await _books.UpdateBook(id, book);
            return Ok();

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {

            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            await _books.DeleteBook(id);
            return Ok();
        }
        [HttpGet]
        [Route("genre/{id}")]
        public async Task<IActionResult> GetBooksByGenre(string name)
        {

            return Ok(_books.GetBooksByGenre(name));
        }

        [HttpGet]
        [Route("author/{author}")]
        public async Task<IActionResult> GetBooksByauthor(string author)
        {


            if (!_books.GetAll().Any(b => b.author == author))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found book with that author")
                });
            }
            var books = _books.GetBooksByAuthor(author);
            return Ok(books);
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetBooksByName(string name, int? page, int? pageSize)
        {

            var books = _books.GetBooksByName(name, page, pageSize);
            if (books.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("not found book with that name")
                });
            }
            return Ok(books);
        }

      
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            if (!_books.BookExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound(new { message = $"Книга с ID {id} не найдена." })
                });
            }

            return Ok(_books.GetBookById(id));

        }
        [HttpPost("uploadpfp")]
        public async Task<IActionResult> UploadProfilePhoto(int readerId, IFormFile photo)
        {
            var url = await _books.UploadProfilePhoto(readerId, photo);
            return new OkObjectResult(new { url = url });
        }
        [HttpPut("updatepfp/{readerId}")]
        public async Task<IActionResult> UpdateProfilePhoto(int readerId, IFormFile photo)
        {
            var url = await _books.UpdateProfilePhoto(readerId, photo);
            return new OkObjectResult(new { url = url });
        }
        [HttpDelete("deletepfp/{readerId}")]
        public async Task<IActionResult> DeleteProfilePhoto(int readerId)
        {
            await _books.DeleteProfilePhoto(readerId);
            return Ok();
        }
    }
}
