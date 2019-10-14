using System.Linq;
using CardCatalog.Data;
using CardCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardCatalog.Controllers
{
    public class MembersController : Controller
    {
        private readonly MemberRepository _memberRepo;

        public MembersController(MemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        public IActionResult Index()
        {
            var models = _memberRepo.GetAll()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new MemberViewModel(x))
                .ToArray();

            return View(models);
        }
    }
}
