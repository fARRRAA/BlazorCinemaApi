﻿using BooksServiceApi.Requests;
using CinemaDigestApi.Model;

namespace CinemaDigestApi.Interfaces
{
    public interface IGenreService
    {
        public List<Genre> GetAllGenres();
        public Task AddNewGenre(CreateGenre createdGenre);
        public Task UpdateGenreById(int id, CreateGenre createdGenre);
        public Task DeleteGenreById(int id);
        public bool GenreExists(int id);
    }
}
