using FilmCatalogue.Models;
using FilmCatalogue.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmCatalogue.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmService _service;
        public FilmsController(FilmService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var films = await _service.GetAllFilmsAsync();
            return View(films);
        }
        public async Task<IActionResult> Details(int id)
        {
            var film = await _service.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }
        public async Task<IActionResult> Create(Film film)
        {
            if (film.GenreId == 0)
            {
                ModelState.AddModelError("GenreId", "Please choose genre");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var genres = await _service.GetGenresAsync();
                    var selectedGenre = genres.FirstOrDefault(g => g.Id == film.GenreId);

                    if (selectedGenre == null)
                    {
                        ModelState.AddModelError("GenreId", "Invalid genre");
                        ViewBag.Genres = genres;
                        return View(film);
                    }

                    film.Genre = selectedGenre;
                    await _service.CreateFilmAsync(film);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }

            var genres1 = await _service.GetGenresAsync();
            ViewBag.Genres = genres1;
            return View(film);
        }

        [HttpGet("/films/edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var film = await _service.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            var genres = await _service.GetGenresAsync();
            ViewBag.Genres = genres;
            return View(film);
        }

        [HttpPost("/films/edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _service.UpdateFilmAsync(film);
                return RedirectToAction(nameof(Index));
            }
            var genres = await _service.GetGenresAsync();
            ViewBag.Genres = genres;
            return View(film);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteFilmAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
