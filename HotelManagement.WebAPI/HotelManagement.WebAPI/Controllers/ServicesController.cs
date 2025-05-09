using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // GET: api/services
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetServices()
        {
            try
            {
                var services = await _serviceRepository.GetServicesWithDetailsAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/services/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetService(int id)
        {
            try
            {
                var service = await _serviceRepository.GetServiceDetailsAsync(id);
                if (service == null)
                {
                    return NotFound();
                }
                return Ok(service);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/services/category/{category}
        [HttpGet]
        [Route("category/{category}")]
        public async Task<IHttpActionResult> GetServicesByCategory(string category)
        {
            try
            {
                var services = await _serviceRepository.GetServicesByCategoryAsync(category);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/services/available
        [HttpGet]
        [Route("available")]
        public async Task<IHttpActionResult> GetAvailableServices()
        {
            try
            {
                var services = await _serviceRepository.GetAvailableServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/services
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateService(Service service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _serviceRepository.AddAsync(service);
                return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/services/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateService(int id, Service service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != service.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingService = await _serviceRepository.GetByIdAsync(id);
                if (existingService == null)
                {
                    return NotFound();
                }

                await _serviceRepository.UpdateAsync(service);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/services/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteService(int id)
        {
            try
            {
                var service = await _serviceRepository.GetByIdAsync(id);
                if (service == null)
                {
                    return NotFound();
                }

                await _serviceRepository.DeleteAsync(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}