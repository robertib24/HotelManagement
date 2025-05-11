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
    [RoutePrefix("api/reservations")]
    public class ReservationsController : ApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReservationsController(
            IReservationRepository reservationRepository,
            IRoomRepository roomRepository,
            ICustomerRepository customerRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/reservations
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetReservations()
        {
            try
            {
                var reservations = await _reservationRepository.GetReservationsWithDetailsAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetReservation(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationDetailsAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/customer/5
        [HttpGet]
        [Route("customer/{customerId:int}")]
        public async Task<IHttpActionResult> GetReservationsByCustomer(int customerId)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return NotFound();
                }

                var reservations = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/hotel/5
        [HttpGet]
        [Route("hotel/{hotelId:int}")]
        public async Task<IHttpActionResult> GetReservationsByHotel(int hotelId)
        {
            try
            {
                var reservations = await _reservationRepository.GetReservationsByHotelAsync(hotelId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/active
        [HttpGet]
        [Route("active")]
        public async Task<IHttpActionResult> GetActiveReservations()
        {
            try
            {
                var reservations = await _reservationRepository.GetActiveReservationsAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/checkins-today
        [HttpGet]
        [Route("checkins-today")]
        public async Task<IHttpActionResult> GetCheckInsForToday()
        {
            try
            {
                var reservations = await _reservationRepository.GetCheckInsForTodayAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/reservations/checkouts-today
        [HttpGet]
        [Route("checkouts-today")]
        public async Task<IHttpActionResult> GetCheckOutsForToday()
        {
            try
            {
                var reservations = await _reservationRepository.GetCheckOutsForTodayAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/reservations
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateReservation(Reservation reservation)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("CreateReservation method called");

                if (!ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Model state is invalid");
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(string.Join(", ", errors));
                }

                // Log reservation details
                System.Diagnostics.Debug.WriteLine($"Reservation details: CustomerId={reservation.CustomerId}, " +
                    $"RoomId={reservation.RoomId}, CheckIn={reservation.CheckInDate}, CheckOut={reservation.CheckOutDate}");

                // Check if customer exists
                var customer = await _customerRepository.GetByIdAsync(reservation.CustomerId);
                if (customer == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Customer with ID {reservation.CustomerId} not found");
                    return BadRequest("Customer not found");
                }

                // Check room availability
                bool isRoomAvailable = await _reservationRepository.IsRoomAvailableForReservationAsync(
                    reservation.RoomId, reservation.CheckInDate, reservation.CheckOutDate);

                if (!isRoomAvailable)
                {
                    System.Diagnostics.Debug.WriteLine($"Room {reservation.RoomId} is not available for the selected dates");
                    return BadRequest("Room is not available for the selected dates");
                }

                // Calculate total price
                decimal totalPrice = await _reservationRepository.CalculateTotalPriceAsync(
                    reservation.RoomId, reservation.CheckInDate, reservation.CheckOutDate);

                reservation.TotalPrice = totalPrice;
                reservation.CreatedAt = DateTime.Now;
                reservation.ReservationStatus = "Confirmed";

                System.Diagnostics.Debug.WriteLine("Adding reservation to repository");

                await _reservationRepository.AddAsync(reservation);
                System.Diagnostics.Debug.WriteLine($"Reservation added successfully with ID: {reservation.Id}");

                // Update room status if check-in is today
                if (reservation.CheckInDate.Date == DateTime.Today)
                {
                    await _roomRepository.SetRoomStatusAsync(reservation.RoomId, true, true, false);
                }

                // Returnăm doar un mesaj simplu și ID-ul, nu întreaga entitate
                return Ok(new
                {
                    Id = reservation.Id,
                    Message = "Reservation created successfully"
                });
            }
            catch (Exception ex)
            {
                // Log exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error in CreateReservation: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
                }

                return InternalServerError(ex);
            }
        }

        // PUT: api/reservations/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateReservation(int id, Reservation reservation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != reservation.Id)
                {
                    return BadRequest("ID mismatch");
                }

                var existingReservation = await _reservationRepository.GetByIdAsync(id);
                if (existingReservation == null)
                {
                    return NotFound();
                }

                // Verifică disponibilitatea camerei dacă s-a schimbat camera sau perioada
                if (existingReservation.RoomId != reservation.RoomId ||
                    existingReservation.CheckInDate != reservation.CheckInDate ||
                    existingReservation.CheckOutDate != reservation.CheckOutDate)
                {
                    bool isRoomAvailable = await _reservationRepository.IsRoomAvailableForReservationAsync(
                        reservation.RoomId, reservation.CheckInDate, reservation.CheckOutDate, id);

                    if (!isRoomAvailable)
                    {
                        return BadRequest("Room is not available for the selected dates");
                    }

                    // Recalculează prețul total
                    decimal totalPrice = await _reservationRepository.CalculateTotalPriceAsync(
                        reservation.RoomId, reservation.CheckInDate, reservation.CheckOutDate);

                    reservation.TotalPrice = totalPrice;
                }
                else
                {
                    // Păstrează prețul original dacă nu s-a schimbat camera sau perioada
                    reservation.TotalPrice = existingReservation.TotalPrice;
                }

                // Păstrează data de creare originală
                reservation.CreatedAt = existingReservation.CreatedAt;

                await _reservationRepository.UpdateAsync(reservation);

                // Actualizează statusul camerei dacă a fost modificat statusul rezervării
                if (existingReservation.ReservationStatus != reservation.ReservationStatus)
                {
                    if (reservation.ReservationStatus == "CheckedIn")
                    {
                        await _roomRepository.SetRoomStatusAsync(reservation.RoomId, true, true, false);
                    }
                    else if (reservation.ReservationStatus == "Completed" || reservation.ReservationStatus == "Cancelled")
                    {
                        await _roomRepository.SetRoomStatusAsync(reservation.RoomId, false, false, false);
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/reservations/5/check-in
        [HttpPost]
        [Route("{id:int}/check-in")]
        public async Task<IHttpActionResult> CheckIn(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                if (reservation.ReservationStatus != "Confirmed")
                {
                    return BadRequest("Reservation is not in Confirmed status");
                }

                reservation.ReservationStatus = "CheckedIn";
                await _reservationRepository.UpdateAsync(reservation);

                // Marchează camera ca fiind ocupată
                await _roomRepository.SetRoomStatusAsync(reservation.RoomId, true, true, false);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/reservations/5/check-out
        [HttpPost]
        [Route("{id:int}/check-out")]
        public async Task<IHttpActionResult> CheckOut(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                if (reservation.ReservationStatus != "CheckedIn")
                {
                    return BadRequest("Reservation is not in CheckedIn status");
                }

                reservation.ReservationStatus = "Completed";
                await _reservationRepository.UpdateAsync(reservation);

                // Marchează camera ca fiind liberă dar necesită curățenie
                await _roomRepository.SetRoomStatusAsync(reservation.RoomId, false, false, false);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/reservations/5/cancel
        [HttpPost]
        [Route("{id:int}/cancel")]
        public async Task<IHttpActionResult> Cancel(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                if (reservation.ReservationStatus != "Confirmed")
                {
                    return BadRequest("Only confirmed reservations can be cancelled");
                }

                reservation.ReservationStatus = "Cancelled";
                await _reservationRepository.UpdateAsync(reservation);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/reservations/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteReservation(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                await _reservationRepository.DeleteAsync(id);

                // Eliberează camera dacă era ocupată de această rezervare
                if (reservation.ReservationStatus == "CheckedIn")
                {
                    await _roomRepository.SetRoomStatusAsync(reservation.RoomId, false, true, false);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}