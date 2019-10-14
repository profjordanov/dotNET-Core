using System.Collections.Generic;

namespace CardCatalog.Data
{
    public class AuthorRepository
    {
        private readonly InMemoryDb _db;

        public AuthorRepository(InMemoryDb db)
        {
            _db = db;
        }

        public IEnumerable<Author> GetAll()
        {
            return _db.Authors;
        }
    }
}