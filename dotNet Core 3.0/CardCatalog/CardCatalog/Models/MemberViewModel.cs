using System.Linq;
using CardCatalog.Data;

namespace CardCatalog.Models
{
    public class MemberViewModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string CheckedOutBooks { get; set; }

        public MemberViewModel(Member member)
        {
            Name = $"{member.FirstName} {member.LastName}";
            Address = member.Address;
            CheckedOutBooks = string.Join(", ", member.CheckedOutItems.Select(x => x.Book.Title));
        }
    }
}