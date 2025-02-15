using CinemaDigestApi.DataBaseContext;
using CinemaDigestApi.Interfaces;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaDigestApi.Service
{
    public class MovieService : IMovieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ContextDb _context;

        public MovieService(ContextDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task AddNewMovie(CreateMovie createdMovie)
        {
            var movie = new Movie()
            {
                genreId = createdMovie.genreId,
                description = createdMovie.description,
                name = createdMovie.name,
                releaseYear = createdMovie.releaseYear,
                duration = createdMovie.duration,
                rating = createdMovie.rating,
                image = createdMovie.image

            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public List<Movie> All()
        {
            return _context.Movies.ToList();
        }

        public async Task DeleteMovieById(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.id == id);
            _context.Remove(movie);
            _context.SaveChanges();
        }

        public List<Movie> GetAllMovies(string? name,string? genre, int? page, int? pageSize)
        {
           IQueryable<Movie> query = _context.Movies.Include(x => x.Genre);

            if (!string.IsNullOrEmpty(name)){
                var nameParts = name.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                query =query.Where(i => nameParts.Any(part => i.name.ToLower().Contains(part))).Include(b => b.Genre);
            }
            if (!string.IsNullOrEmpty(genre))
            {
                query=query.Where(x => x.Genre.name.ToLower()==genre);

            }
            if(page.HasValue && pageSize.HasValue)
            {
                return query.Skip((int)(page-1) *(int)pageSize).Take((int)pageSize).ToList();
            }
            return query.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return  _context.Movies.Include(x=>x.Genre).FirstOrDefault(x => x.id == id);
        }

        public List<Movie> GetMoviesByGenre(string name)
        {
            return _context.Movies.Include(x => x.Genre).Where(x => x.Genre.name.ToLower().Contains(name.ToLower())).ToList();
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(x => x.id == id);
        }

        public async Task UpdateMovieById(int id, CreateMovie createdMovie)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.id == id);
            movie.genreId = createdMovie.genreId;
            movie.description = createdMovie.description;
            movie.name = createdMovie.name;
            movie.releaseYear = createdMovie.releaseYear;
            movie.duration = createdMovie.duration;
            movie.rating = createdMovie.rating;
            movie.image = createdMovie.image;
            await _context.SaveChangesAsync();
        }
    }
}
