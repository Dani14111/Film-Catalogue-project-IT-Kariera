using FilmCatalogue.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Film> Films { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Film>()
            .HasOne(f => f.Genre)
            .WithMany(g => g.Films)
            .HasForeignKey(f => f.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Drama" },
            new Genre { Id = 2, Name = "Criminal" },
            new Genre { Id = 3, Name = "Fantasy" }
        );

        builder.Entity<Film>().HasData(
            new Film { Id = 1, Title = "Inception", GenreId = 3, ReleaseDate = new DateTime(2010, 7, 16), Studio = "Warner Bros", Score = 95 },
            new Film { Id = 2, Title = "The Godfather", GenreId = 2, ReleaseDate = new DateTime(1972, 3, 24), Studio = "Paramount", Score = 98 }
        );
    }
}