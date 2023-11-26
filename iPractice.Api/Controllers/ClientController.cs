using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Helpers;
using iPractice.Api.Models;
using iPractice.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IAvailabilityService _availabilityService;
        private readonly IBookingService _bookingService;
        
        public ClientController(ILogger<ClientController> logger, IAvailabilityService availabilityService, IBookingService bookingService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
            _bookingService = bookingService;
        }
        
        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlot>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlots([FromRoute] long clientId)
        {
            var availabilities = await _availabilityService.GetAvailableTimeSlotsAsync(clientId);
            return Ok(Mapper.MapTimeSlots(availabilities));
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="timeSlot">Identifies the client and availability slot</param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateAppointment([FromRoute] long clientId, [FromBody] TimeSlot timeSlot)
        {
            try
            {
                var existingTimeSlot = await _bookingService.GetTimeSlotAsync(timeSlot.Id);
                if (existingTimeSlot == null)
                {
                    return NotFound("Resource with given parameters was not found!");
                }

                await _bookingService.CreateAppointmentAsync(clientId, timeSlot.Id);
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity creation. Please try again later.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
