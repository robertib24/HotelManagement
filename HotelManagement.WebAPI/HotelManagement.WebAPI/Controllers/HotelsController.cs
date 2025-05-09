using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Controllers
{
    [RoutePrefix("api/hotels")]
    public class HotelsController : ApiController
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        public HotelsController(IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
        }

        // GET: api/hotels
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetHotels()
        {
            try
            {
                var hotels = await _hotelRepository.GetHotelsWithDetailsAsync();
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/hotels/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _hotelRepository.GetHotelDetailsAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/hotels/5/rooms
        [HttpGet]
        [Route("{id:int}/rooms")]
        public async Task<IHttpActionResult> GetHotelRooms(int id)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }

                var rooms = await _roomRepository.GetRoomsByHotelAsync(id);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/hotels
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateHotel(Hotel hotel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _hotelRepository.AddAsync(hotel);
                return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/hotels/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateHotel(int id, Hotel hotel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != hotel.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingHotel = await _hotelRepository.GetByIdAsync(id);
                if (existingHotel == null)
                {
                    return NotFound();
                }

                await _hotelRepository.UpdateAsync(hotel);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/hotels/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteHotel(int id)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }

                await _hotelRepository.DeleteAsync(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}