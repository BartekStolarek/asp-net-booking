using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingCinema.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookingCinema.Controllers
{
    [Authorize(Roles="ADMINISTRATOR")]
    public class BookedMoviesController : Controller
    {
        private readonly BookingCinemaContext _context;

        public BookedMoviesController(BookingCinemaContext context)
        {
            _context = context;
        }

        // GET: BookedMovies
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookedMovies.ToListAsync());
        }

        // GET: BookedMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedMovies = await _context.BookedMovies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookedMovies == null)
            {
                return NotFound();
            }

            return View(bookedMovies);
        }

        // GET: BookedMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookedMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,MovieID,MovieName,TakenSeats")] BookedMovies bookedMovies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookedMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookedMovies);
        }

        // GET: BookedMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedMovies = await _context.BookedMovies.FindAsync(id);
            if (bookedMovies == null)
            {
                return NotFound();
            }
            return View(bookedMovies);
        }

        // POST: BookedMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,MovieID,MovieName,TakenSeats")] BookedMovies bookedMovies)
        {
            if (id != bookedMovies.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookedMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedMoviesExists(bookedMovies.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookedMovies);
        }

        // GET: BookedMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedMovies = await _context.BookedMovies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookedMovies == null)
            {
                return NotFound();
            }

            return View(bookedMovies);
        }

        // POST: BookedMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookedMovies = await _context.BookedMovies.FindAsync(id);
            _context.BookedMovies.Remove(bookedMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookedMoviesExists(int id)
        {
            return _context.BookedMovies.Any(e => e.ID == id);
        }
    }
}
