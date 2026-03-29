using FilmCatalogue.Data;
using FilmCatalogue.Models;

namespace FilmCatalogue.Services
{
    public class FilmService
    {
        private readonly IFilmRepository _repository;
        public FilmService(IFilmRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Film>> GetAllFilmsAsync()
        {
            var films = await _repository.GetAllAsync();
            return films.Where(f => f.Score >= 0).OrderByDescending(f => f.Score).ToList();
        }
        public async Task<Film> CreateFilmAsync(Film film)
        {
            if (string.IsNullOrWhiteSpace(film.Title))
                throw new ArgumentException("Title can't be empty.");
            if (film.Score < 0 || film.Score > 100)
                throw new ArgumentException("Rating must be between 0 and 100.");
            if (film.GenreId <= 0)
                throw new ArgumentException("Choose a genre.");
            return await _repository.AddAsync(film);
        }
        public async Task<Film?> GetFilmByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task UpdateFilmAsync(Film film)
        {
            await _repository.UpdateAsync(film);
        }
        public async Task DeleteFilmAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _repository.GetGenresAsync();
        }
    }
}
