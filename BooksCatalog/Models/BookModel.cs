namespace BooksCatalog.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public virtual AuthorModel? Author { get; set; }
    }
}
