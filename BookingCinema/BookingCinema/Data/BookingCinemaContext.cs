using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingCinema.Models
{
    public class BookingCinemaContext : DbContext
    {
        public BookingCinemaContext (DbContextOptions<BookingCinemaContext> options)
            : base(options)
        {
        }

        public DbSet<BookingCinema.Models.Movie> Movie { get; set; }
    }
}
