using BlueYonder.Hotels.DAL.Database;
using BlueYonder.Hotels.DAL.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlueYonder.Hotels.DAL.Repository
{
    public class HotelBookingRepository
    {
        private DbContextOptions<HotelsContext> _options;

        public HotelBookingRepository(DbContextOptions<HotelsContext> options = null)
        {
            _options = options;
        }

        public IEnumerable<Room> GetAvailabilityByDate(DateTime date)
        {
            using (HotelsContext context = new HotelsContext(_options))
            {
                var roomsWithoutReservation =
                    (from room in context.Rooms
                     join reservation in context.Reservations on room equals reservation.Room into result
                     from reservation in result.DefaultIfEmpty()
                     where date < reservation.CheckIn || date > reservation.CheckOut || room.Reservations.Count
                     select room)
                    .ToList();

                return roomsWithoutReservation;
            }
         }
        
        public IEnumerable<Reservation> GetAllReservation()
        {
            using (HotelsContext context = new HotelsContext(_options))
            {
                var reservationBooking = context.Reservations;
                return reservationBooking?.ToList();
            }
        }

        public async Task DeleteReservation(int reservationId)
        {
            using (HotelsContext context = new HotelsContext(_options))
            {
                var reservation = context.Reservations.FirstOrDefault(b => b.ReservationId == reservationId);
                if (reservation != null)
                {
                    context.Reservations.Remove(reservation);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
