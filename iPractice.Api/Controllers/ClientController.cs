using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Domain.Models;
using iPractice.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IAppointmentsService _appointmentsService;
        private readonly IAvailabilityService _availabilityService;

        public ClientController(
            ILogger<ClientController> logger, 
            IAppointmentsService appointmentsService,
            IAvailabilityService availabilityService)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
            _availabilityService = availabilityService;
        }

        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlot>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlots(long clientId)
        {
            var availabilities = await _availabilityService.GetAsync(clientId);
            
            var timeSlotResult = new List<TimeSlot>();
            foreach (var availability in availabilities)
            {
                timeSlotResult.Add(new TimeSlot
                {
                    PsychologistId = availability.PsychologistId,
                    Start = availability.Start,
                    End = availability.End
                });
            }

            return Ok(timeSlotResult);
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="timeSlot">Identifies the client and availability slot</param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateAppointment(long clientId, [FromBody] TimeSlot timeSlot)
        {
            var result = await _appointmentsService.CreateAsync(new Appointment
            {
                ClientId = clientId,
                PsychologistId = timeSlot.PsychologistId,
                Start = timeSlot.Start,
                End = timeSlot.End,
            });

            return Ok(result);
        }
    }
}
