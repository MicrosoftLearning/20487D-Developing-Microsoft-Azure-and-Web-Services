using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Hotels.DAL.Models;
using BlueYonder.Hotels.DAL.Repository;
using BlueYonder.Hotels.Service.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BlueYonderHotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly HotelBookingRepository _hotelBookingRepository;

        public HotelBookingController()
        {
            _hotelBookingRepository = new HotelBookingRepository();
        }


        [HttpGet("Availability/{date}")]
        public IEnumerable<Room> GetAvailability(DateTime date)
        {
            return _hotelBookingRepository.GetAvailabilityByDate(date);
        }
        
        [HttpGet("Reservation")]
        public IEnumerable<Reservation> GetReservation()
        {
            return _hotelBookingRepository.GetAllReservation();
        }

        
        [HttpDelete("Reservation/{reservationId}")]
        public async Task DeleteReservation(int reservationId)
        {
            await _hotelBookingRepository.DeleteReservation(reservationId);
        }
       
    }
}
