namespace BooksCatalog.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<BookModel>? Books { get; set; }
    }
}
