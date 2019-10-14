using System.Collections.Generic;

namespace CardCatalog.Data
{
    public class BookRepository
    {
        private readonly InMemoryDb _db;

        public BookRepository(InMemoryDb db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetAll()
        {
            return _db.Books;
        }
    }
}