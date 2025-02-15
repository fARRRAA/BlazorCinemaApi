using BooksServiceApi.Requests;
using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaDigestApi.Service
{
    public class GenresService : IGenreService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public GenresService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public List<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public async Task AddNewGenre(CreateGenre createdGenre)
        {
            var check = _context.Genres.FirstOrDefaultAsync(i => i.name.ToLower() == createdGenre.name.ToLower());
            var Genres = new Genre() { name = createdGenre.name };
            await _context.Genres.AddAsync(Genres);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenreById(int id, CreateGenre createdGenre)
        {
            var Genres = await _context.Genres.FirstOrDefaultAsync(i => i.id == id);
            Genres.name = createdGenre.name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreById(int id)
        {
            var Genres = await _context.Genres.FirstOrDefaultAsync(i => i.id == id);
            _context.Genres.Remove(Genres);
            _context.SaveChanges();
        }

        public bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.id == id);
        }
    }
}
