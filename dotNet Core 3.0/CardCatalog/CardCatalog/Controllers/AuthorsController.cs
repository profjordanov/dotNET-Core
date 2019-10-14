using System.Linq;
using CardCatalog.Data;
using CardCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardCatalog.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorRepository _authorRepo;

        public AuthorsController(AuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IActionResult Index()
        {
            var models = _authorRepo.GetAll()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new AuthorViewModel(x))
                .ToArray();

            return View(models);
        }
    }
}
