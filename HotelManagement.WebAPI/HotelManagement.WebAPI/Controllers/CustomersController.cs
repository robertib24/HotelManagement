using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HotelManagement.WebAPI.Models;
using HotelManagement.WebAPI.Repositories;

namespace HotelManagement.WebAPI.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/customers
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetCustomersWithDetailsAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/customers/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerDetailsAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/customers/vip
        [HttpGet]
        [Route("vip")]
        public async Task<IHttpActionResult> GetVIPCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetVIPCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/customers/email/{email}
        [HttpGet]
        [Route("email/{email}")]
        public async Task<IHttpActionResult> GetCustomerByEmail(string email)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(email);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/customers/5/reservations
        [HttpGet]
        [Route("{id:int}/reservations")]
        public async Task<IHttpActionResult> GetCustomerReservations(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                var reservations = await _customerRepository.GetCustomerReservationsAsync(id);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/customers
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verifică dacă există deja un client cu același email sau ID number
                var existingByEmail = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
                if (existingByEmail != null)
                {
                    return BadRequest("A customer with this email already exists");
                }

                var existingByIdNumber = await _customerRepository.GetCustomerByIdNumberAsync(customer.IdNumber);
                if (existingByIdNumber != null)
                {
                    return BadRequest("A customer with this ID number already exists");
                }

                customer.RegistrationDate = DateTime.Now;
                await _customerRepository.AddAsync(customer);
                return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/customers/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != customer.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingCustomer = await _customerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                // Verificăm email-ul și ID-ul pentru a evita duplicatele
                if (existingCustomer.Email != customer.Email)
                {
                    var existingByEmail = await _customerRepository.GetCustomerByEmailAsync(customer.Email);
                    if (existingByEmail != null)
                    {
                        return BadRequest("A customer with this email already exists");
                    }
                }

                if (existingCustomer.IdNumber != customer.IdNumber)
                {
                    var existingByIdNumber = await _customerRepository.GetCustomerByIdNumberAsync(customer.IdNumber);
                    if (existingByIdNumber != null)
                    {
                        return BadRequest("A customer with this ID number already exists");
                    }
                }

                // Păstrăm data de înregistrare originală
                customer.RegistrationDate = existingCustomer.RegistrationDate;

                await _customerRepository.UpdateAsync(customer);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/customers/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                await _customerRepository.DeleteAsync(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}