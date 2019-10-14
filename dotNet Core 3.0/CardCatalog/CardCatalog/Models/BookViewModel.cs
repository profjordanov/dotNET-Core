using System;
using CardCatalog.Data;

namespace CardCatalog.Models
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedAt { get; set; }
        public BookGenre Genre { get; set; }
        public DateTime? CheckedOutUntil { get; set; }

        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            AuthorName = $"{book.Author.FirstName} {book.Author.LastName}";
            PublishedAt = book.PublishedAt;
            Genre = book.Genre;
            CheckedOutUntil = book.ActiveCheckOut?.DueDate;
        }
    }
}