using System;
using System.Collections.Generic;
using System.Linq;

namespace CardCatalog.Data
{
    public class InMemoryDb
    {
        public List<Author> Authors { get; } = new List<Author>();
        public List<Book> Books { get; } = new List<Book>();
        public List<Member> Members { get; } = new List<Member>();

        public InMemoryDb()
        {
            BuildAuthors();

            BuildBooks();

            BuildMembers();
        }

        private void BuildMembers()
        {
            Members.AddRange(new[]
            {
                new Member("Kerry", "Winsome", "860 Jay Road Birmingham AL"),
                new Member("Meg", "Sidney", "79483 Bartillon Terrace Peoria IL"),
                new Member("Rori", "Duncan", "49 Delaware Plaza El Paso TX"),
                new Member("Rodney", "Ackers", "39165 Glacier Hill Drive Pittsburgh PA"),
            });

            Members[0].CheckoutBook(Books[0], DateFor(2019, 7, 10));
            Members[0].CheckoutBook(Books[1], DateFor(2019, 7, 10));
            Members[2].CheckoutBook(Books[4], DateFor(2019, 8, 1));
            Members[3].CheckoutBook(Books[6], DateFor(2019, 8, 24));
        }

        private void BuildBooks()
        {
            Books.AddRange(new []
            {
                new Book("Binary New World", Authors[0], DateFor(2010, 6, 15)),
                new Book("Infinite Loop", Authors[1], DateFor(2012, 7, 30)),
                new Book("The Hex Files", Authors[1], DateFor(2013, 9, 5)),
                new Book("Jane Error", Authors[2], DateFor(2014, 1, 26)),
                new Book("Catch 10110", Authors[3], DateFor(2015, 2, 11)),
                new Book("Lord of the Stack", Authors[3], DateFor(2015, 7, 19)),
                new Book("Try Catch Finally", Authors[3], DateFor(2016, 3, 28)),
            });

            foreach (var book in Books)
            {
                AssignBookToAuthor(book);
            }
        }

        private void BuildAuthors()
        {
            Authors.AddRange(new[]
            {
                new Author("Stacey", "Spear", BookGenre.NonFiction),
                new Author("Justin", "Patton", BookGenre.SciFi),
                new Author("Allen", "Cardwell", BookGenre.Technical),
                new Author("Luke", "Wolfe", BookGenre.History),
            });
        }

        private void AssignBookToAuthor(Book book)
        {
            var author = Authors.FirstOrDefault(x => !x.Books.Any()) ??
                         Authors.OrderBy(x => Guid.NewGuid()).First();
            book.Author = author;
            book.Genre = author.Genre;
            author.Books.Add(book);
        }

        private DateTime DateFor(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }
    }
}
