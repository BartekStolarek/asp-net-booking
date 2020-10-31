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
    //[Authorize(Roles="ADMINISTRATOR")]
    public class MoviesController : Controller
    {
        private readonly BookingCinemaContext _context;

        public MoviesController(BookingCinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "ADMINISTRATOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,TakenSeats")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            
            if (movie == null)
            {
                return NotFound();
            } else
            {
                // Decode takenSeats
                var takenSeats = movie.TakenSeats;
                if (takenSeats == null)
                {
                    ViewBag.decodedTakenSeats = new string[0];
                } else
                {
                    String[] decodedTakenSeats = takenSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ViewBag.decodedTakenSeats = decodedTakenSeats;
                }
                //string[] allSeats = new string[20] as string[];
                // var allSeats = ["1", "2", "3"];
                //for (int i = 0; i < allSeats.Length; i++)
                //{
                //    allSeats[i] = i.ToString();
                //}
                // Console.WriteLine(allSeats);
                //String[] allSeats = new string[]{ "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };

                //ViewBag.availableSeats = allSeats.Except(decodedTakenSeats);
                //Console.WriteLine(ViewBag.availableSeats);
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,TakenSeats")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (User.IsInRole("ADMINISTRATOR"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(movie);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MovieExists(movie.ID))
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
            } else
            {
                var foundMovie = await _context.Movie.FindAsync(id);

                if (movie == null)
                {
                    return NotFound();
                }

                // Check if selected seats by user were not taken before
                // TODO

                foundMovie.TakenSeats = movie.TakenSeats;

                /*Console.WriteLine(".............................");
                Console.WriteLine(movie);
                Console.WriteLine(movie.ID);
                Console.WriteLine(movie.TakenSeats);
                Console.WriteLine(movie.Title);
                Console.WriteLine(".............................");*/
                try
                {
                    _context.Update(foundMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(foundMovie.ID))
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
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookSeat(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,TakenSeats")] Movie movie)
        {
            /*
            if (id != movie.ID)
            {
                return NotFound();
            }

            //if (User.IsInRole("ADMINISTRATOR"))
            //{
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
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
            //} else
            // {
            //    Console.WriteLine(movie);
            //}
            */
            Console.WriteLine(movie);

            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "ADMINISTRATOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
