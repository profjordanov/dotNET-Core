#nullable enable
using System;
using System.Collections.Generic;

namespace CardCatalog.Data
{
    public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BookGenre Genre { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public Author(string firstName, string lastName, BookGenre genre)
        {
            FirstName = firstName;
            LastName = lastName;
            Genre = genre;
        }
    }

    public enum BookGenre
    {
        NonFiction,
        SciFi,
        Technical,
        History
    }

    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public Author Author { get; set; }
        public DateTime PublishedAt { get; set; }
        public BookGenre Genre { get; set; }
        public CheckOut? ActiveCheckOut { get; set; }

        public Book(string title, Author author, DateTime publishedAt)
        {
            Title = title;
            Author = author;
            PublishedAt = publishedAt;
            Genre = author.Genre;
        }
    }

    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<CheckOut> CheckedOutItems { get; set; } = new List<CheckOut>();

        public Member(string firstName, string lastName, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public void CheckoutBook(Book book, DateTime dateCheckedOut)
        {
            var checkOut = new CheckOut(book, dateCheckedOut, dateCheckedOut.Add(TimeSpan.FromDays(7)));
            CheckedOutItems.Add(checkOut);
            book.ActiveCheckOut = checkOut;
        }
    }

    public class CheckOut
    {
        public DateTime CheckedOutAt { get; set; }
        public DateTime DueDate { get; set; }
        public Book Book { get; set; }

        public CheckOut(Book book, DateTime checkedOutAt, DateTime dueDate)
        {
            Book = book;
            CheckedOutAt = checkedOutAt;
            DueDate = dueDate;
        }
    }

}
#nullable restore