using System.Linq;
using CardCatalog.Data;

namespace CardCatalog.Models
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public BookGenre Genre { get; set; }
        public string LatestBook { get; set; }

        public AuthorViewModel(Author author)
        {
            Name = author.FirstName + " " + author.LastName;
            Genre = author.Genre;
            LatestBook = author.Books.OrderByDescending(x => x.PublishedAt)
                .Select(x => x.Title)
                .FirstOrDefault();
        }
    }
}
