using System.Collections.Generic;

namespace CardCatalog.Data
{
    public class MemberRepository
    {
        private readonly InMemoryDb _db;

        public MemberRepository(InMemoryDb db)
        {
            _db = db;
        }

        public IEnumerable<Member> GetAll()
        {
            return _db.Members;
        }
    }
}