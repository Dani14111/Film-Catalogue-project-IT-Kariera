using FilmCatalogue.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FilmCatalogue.Data
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Film>> GetAllAsync()
        {
            return await _context.Films
        .Include(f => f.Genre)
        .OrderByDescending(f => f.Score)
        .ToListAsync();
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.Include(f => f.Genre).FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<Film> AddAsync(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            await _context.Entry(film).Reference(f => f.Genre).LoadAsync();
            return film;
        }

        public async Task UpdateAsync(Film film)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var film = await GetByIdAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
