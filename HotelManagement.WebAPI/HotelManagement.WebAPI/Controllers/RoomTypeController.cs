using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Controllers
{
    [RoutePrefix("api/roomtypes")]
    public class RoomTypesController : ApiController
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypesController(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        // GET: api/roomtypes
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetRoomTypes()
        {
            try
            {
                var roomTypes = await _roomTypeRepository.GetAllAsync();
                return Ok(roomTypes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/roomtypes/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetRoomType(int id)
        {
            try
            {
                var roomType = await _roomTypeRepository.GetByIdAsync(id);
                if (roomType == null)
                {
                    return NotFound();
                }
                return Ok(roomType);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/roomtypes
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateRoomType(RoomType roomType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _roomTypeRepository.AddAsync(roomType);
                return CreatedAtRoute("DefaultApi", new { id = roomType.Id }, roomType);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/roomtypes/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateRoomType(int id, RoomType roomType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != roomType.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingRoomType = await _roomTypeRepository.GetByIdAsync(id);
                if (existingRoomType == null)
                {
                    return NotFound();
                }

                await _roomTypeRepository.UpdateAsync(roomType);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/roomtypes/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteRoomType(int id)
        {
            try
            {
                var roomType = await _roomTypeRepository.GetByIdAsync(id);
                if (roomType == null)
                {
                    return NotFound();
                }

                await _roomTypeRepository.DeleteAsync(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}