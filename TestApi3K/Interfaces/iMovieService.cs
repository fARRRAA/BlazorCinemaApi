using BooksServiceApi.Requests;
using CinemaDigestApi.Model;
using CinemaDigestApi.Requests;

namespace CinemaDigestApi.Interfaces
{
    public interface IMovieService
    {
        public List<Movie> GetAllMovies(string? name,string? genre,int? page,int? pageSize);
        public Task AddNewMovie(CreateMovie createdMovie);
        public Task UpdateMovieById(int id, CreateMovie createdMovie);
        public Task DeleteMovieById(int id);
        public bool MovieExists(int id);
        public Movie GetMovieById(int id);
        public List<Movie> GetMoviesByGenre(string name);
        public List<Movie> All();
    }
}
