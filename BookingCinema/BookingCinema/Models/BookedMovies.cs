using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingCinema.Models
{
    public class BookedMovies
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string TakenSeats { get; set; }
    }
}
