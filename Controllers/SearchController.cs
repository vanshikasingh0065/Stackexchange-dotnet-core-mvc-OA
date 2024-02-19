using CahootSOOA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CahootSOOA.Controllers
{
    public class SearchController : Controller
    {
        private readonly StackOverflow2010Context _context;

        public SearchController(StackOverflow2010Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchQuery, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = "htmlerror"; // Set default search query
            }

            int pageSize = 10;
            // Assuming _context is your database context
            /*var posts = await _context.Posts
                // Include necessary navigation properties and apply filtering based on searchQuery
                // This is a simplified example. Adjust according to your actual query logic.
                .Where(p => p.Title.Contains(searchQuery) || p.Body.Contains(searchQuery))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();*/

            // Assuming SearchViewModel is your view model
            var viewModel = new SearchViewModel
            {
                Posts = [],
                SearchQuery = searchQuery,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)_context.Posts.Count() / pageSize)
            };

            return View(viewModel);
        }


    }
}
