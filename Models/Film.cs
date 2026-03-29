namespace FilmCatalogue.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Studio { get; set; } = "";
        public int Score { get; set; }
    }
}
