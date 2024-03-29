﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingCinema.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BookingCinema.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly BookingCinemaContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

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
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,TakenSeats,ImageUrl")] Movie movie)
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,TakenSeats,ImageUrl")] Movie movie)
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
                var previouslySelectedSeats = foundMovie.TakenSeats;
                var currentlySelectedSeats = movie.TakenSeats;
                var userSelectedSeats = "";

                if (currentlySelectedSeats != null)
                {
                    if (previouslySelectedSeats != null)
                    {
                        String[] decodedPreviouslySelectedSeats = previouslySelectedSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        String[] decodedCurrentlySelectedSeats = currentlySelectedSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        var excluded = decodedCurrentlySelectedSeats.Except(decodedPreviouslySelectedSeats);
                        userSelectedSeats = string.Join(",", excluded);
                    } else
                    {
                        userSelectedSeats = currentlySelectedSeats;
                    }
                } else
                {
                    userSelectedSeats = previouslySelectedSeats;
                }

                foundMovie.TakenSeats = movie.TakenSeats;

                // Update takenSeats in movie
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

                // Update BookedMovies table
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var movieID = movie.ID;
                var movieName = foundMovie.Title;
                var takenSeatsByUser = movie.TakenSeats;

                try
                {
                    var bookedMovie = new BookedMovies();
                    bookedMovie.MovieID = movieID;
                    bookedMovie.UserID = userID;
                    bookedMovie.MovieName = movieName;
                    bookedMovie.TakenSeats = userSelectedSeats;

                    _context.Add<BookedMovies>(bookedMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Index", "Home");
            }
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

        // private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
