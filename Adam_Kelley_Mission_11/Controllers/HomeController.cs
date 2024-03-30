using Adam_Kelley_Mission_11.Models;
using Adam_Kelley_Mission_11.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adam_Kelley_Mission_11.Controllers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository _repo;

        // Constructor injection of IBookStoreRepository
        public HomeController(IBookStoreRepository temp)
        {
            _repo = temp;
        }

        // Action method for rendering the home page with pagination
        public IActionResult Index(int pageNum)
        {
            int pageSize = 10; // Number of items per page

            // Retrieving books from the repository, applying pagination
            var viewModel = new BooksListViewModel
            {
                Books = _repo.Books
                    .OrderBy(x => x.Title) // Order books by title
                    .Skip((pageNum - 1) * pageSize) // Skip pages based on current page number
                    .Take(pageSize), // Take only the required number of items per page

                // Pagination information
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum, // Current page number
                    ItemsPerPage = pageSize, // Number of items per page
                    TotalItems = _repo.Books.Count() // Total number of items in the repository
                }
            };

            // Render the view with the constructed view model
            return View(viewModel);
        }
    }
}
