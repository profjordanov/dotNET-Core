using System.Linq;
using CardCatalog.Data;
using CardCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardCatalog.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepo;

        public BooksController(BookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IActionResult Index()
        {
            var models = _bookRepo.GetAll()
                .OrderBy(x => x.Title)
                .Select(x => new BookViewModel(x))
                .ToArray();

            return View(models);
        }
    }
}
