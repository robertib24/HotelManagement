using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Controllers
{
    [RoutePrefix("api/rooms")]
    public class RoomsController : ApiController
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: api/rooms
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetRoomsWithDetailsAsync();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/rooms/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetRoom(int id)
        {
            try
            {
                var room = await _roomRepository.GetRoomDetailsAsync(id);
                if (room == null)
                {
                    return NotFound();
                }
                return Ok(room);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/rooms/5/isAvailable
        [HttpGet]
        [Route("{id:int}/isAvailable")]
        public async Task<IHttpActionResult> IsRoomAvailable(int id, [FromUri] DateTime checkIn, [FromUri] DateTime checkOut)
        {
            try
            {
                if (checkIn >= checkOut)
                {
                    return BadRequest("Check-out date must be after check-in date");
                }

                var isAvailable = await _roomRepository.IsRoomAvailableAsync(id, checkIn, checkOut);
                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/rooms
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateRoom(Room room)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _roomRepository.AddAsync(room);
                return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/rooms/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateRoom(int id, Room room)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != room.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingRoom = await _roomRepository.GetByIdAsync(id);
                if (existingRoom == null)
                {
                    return NotFound();
                }

                await _roomRepository.UpdateAsync(room);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PATCH: api/rooms/5/status
        [HttpPatch]
        [Route("{id:int}/status")]
        public async Task<IHttpActionResult> UpdateRoomStatus(int id, [FromBody] RoomStatusUpdate status)
        {
            try
            {
                var room = await _roomRepository.GetByIdAsync(id);
                if (room == null)
                {
                    return NotFound();
                }

                await _roomRepository.SetRoomStatusAsync(id, status.IsOccupied, status.IsClean, status.NeedsRepair);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/rooms/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteRoom(int id)
        {
            try
            {
                var room = await _roomRepository.GetByIdAsync(id);
                if (room == null)
                {
                    return NotFound();
                }

                await _roomRepository.DeleteAsync(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    public class RoomStatusUpdate
    {
        public bool IsOccupied { get; set; }
        public bool IsClean { get; set; }
        public bool NeedsRepair { get; set; }
    }
}