using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCinema.Controllers
{
    [Authorize]
    public class BookSeat : Controller
    {
        // GET: BookSeat
        public ActionResult Index()
        {


            return View();
        }

        // GET: BookSeat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookSeat/Create
        public ActionResult Create()
        {
            return View();
        }

        /*
        // POST: BookSeat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookSeat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookSeat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookSeat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookSeat/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
