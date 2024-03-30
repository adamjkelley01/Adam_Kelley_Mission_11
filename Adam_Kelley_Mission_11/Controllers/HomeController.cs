using Adam_Kelley_Mission_11.Models;
using Adam_Kelley_Mission_11.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adam_Kelley_Mission_11.Controllers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository _repo;
        public HomeController(IBookStoreRepository temp) 
        {
            _repo = temp;
        }
        public IActionResult Index(int pageNum)
        {
            int pageSize = 3;

            var blah = new BooksListViewModel
            {
                Books = _repo.Books
                .OrderBy(x => x.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Books.Count()
                }
            };
            return View(blah);
        }
    }
}
