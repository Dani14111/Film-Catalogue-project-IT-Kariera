namespace FilmCatalogue.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Film> Films { get; set; } = new();
    }
}
