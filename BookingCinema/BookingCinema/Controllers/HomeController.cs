using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookingCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookingCinemaContext _context;

        public HomeController(ILogger<HomeController> logger, BookingCinemaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Grab list of user's bookedMovies
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookedMovies = from m in _context.BookedMovies
                               select m;
            if (!string.IsNullOrEmpty(userID))
            {
                bookedMovies = bookedMovies.Where(s => s.UserID.Contains(userID));
                return View(await bookedMovies.ToListAsync());
            } else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
