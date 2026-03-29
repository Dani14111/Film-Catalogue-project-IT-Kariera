using FilmCatalogue.Models;

namespace FilmCatalogue.Data
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetAllAsync();
        Task<Film?> GetByIdAsync(int id);
        Task<Film> AddAsync(Film film);
        Task UpdateAsync(Film film);
        Task DeleteAsync(int id);
        Task<List<Genre>> GetGenresAsync();
    }
}
