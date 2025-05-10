using System;
using System.Collections.Generic;
using System.Linq;
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
                System.Diagnostics.Debug.WriteLine($"Error in GetRooms: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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
                System.Diagnostics.Debug.WriteLine($"Error in GetRoom: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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
                System.Diagnostics.Debug.WriteLine($"Error in IsRoomAvailable: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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
                System.Diagnostics.Debug.WriteLine("CreateRoom method called");

                if (!ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Model state is invalid");
                    return BadRequest(ModelState);
                }

                System.Diagnostics.Debug.WriteLine("Adding room to repository");
                await _roomRepository.AddAsync(room);
                System.Diagnostics.Debug.WriteLine($"Room added successfully with ID: {room.Id}");

                // Returnează doar un mesaj de succes și ID-ul, nu entitatea completă
                return Ok(new { Id = room.Id, Message = "Room created successfully" });
            }
            catch (Exception ex)
            {
                // Log excepția pentru debugging
                System.Diagnostics.Debug.WriteLine($"Error in CreateRoom: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
                }

                // Returnează un mesaj de eroare mai descriptiv
                return InternalServerError(new Exception("An error occurred while creating the room. Check logs for details."));
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

                // Returnează un mesaj de succes în loc de entitate
                return Ok(new { Message = "Room updated successfully" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateRoom: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }

                return InternalServerError(new Exception("An error occurred while updating the room. Check logs for details."));
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
                System.Diagnostics.Debug.WriteLine($"Error in UpdateRoomStatus: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return InternalServerError(ex);
            }
        }

        // GET: api/rooms/byHotel/5
        [HttpGet]
        [Route("byHotel/{hotelId:int}")]
        public async Task<IHttpActionResult> GetRoomsByHotel(int hotelId)
        {
            try
            {
                var rooms = await _roomRepository.GetRoomsByHotelAsync(hotelId);
                if (rooms == null || !rooms.Any())
                {
                    return NotFound();
                }
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetRoomsByHotel: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return InternalServerError(ex);
            }
        }

        // GET: api/rooms/available?hotelId=1&roomTypeId=2&checkIn=2025-05-10&checkOut=2025-05-11
        [HttpGet]
        [Route("available")]
        public async Task<IHttpActionResult> GetAvailableRooms(
            [FromUri] int hotelId,
            [FromUri] int? roomTypeId = null,
            [FromUri] DateTime? checkIn = null,
            [FromUri] DateTime? checkOut = null)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetAvailableRooms called with hotelId={hotelId}, roomTypeId={roomTypeId}, checkIn={checkIn}, checkOut={checkOut}");

                if (!checkIn.HasValue || !checkOut.HasValue)
                {
                    return BadRequest("Both checkIn and checkOut dates are required");
                }

                if (checkIn.Value >= checkOut.Value)
                {
                    return BadRequest("Check-out date must be after check-in date");
                }

                var rooms = await _roomRepository.GetAvailableRoomsAsync(
                    hotelId,
                    roomTypeId,
                    checkIn.Value,
                    checkOut.Value);

                System.Diagnostics.Debug.WriteLine($"Found {rooms.Count()} available rooms");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAvailableRooms: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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
                System.Diagnostics.Debug.WriteLine($"Error in DeleteRoom: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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