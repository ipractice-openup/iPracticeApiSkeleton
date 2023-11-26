using System;
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
    public class PsychologistController : ControllerBase
    {
        private readonly ILogger<PsychologistController> _logger;
        private readonly IAvailabilityService _availabilityService;

        public PsychologistController(ILogger<PsychologistController> logger, IAvailabilityService availabilityService)
        {
            _logger = logger;
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public string Get()
        {
            return "Success!";
        }

        /// <summary>
        /// Add a block of time during which the psychologist is available during normal business hours
        /// </summary>
        /// <param name="psychologistId"></param>
        /// <param name="availability">Availability</param>
        /// <returns>Ok if the availability was created</returns>
        [HttpPost("{psychologistId}/availability")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateAvailability([FromRoute] long psychologistId, [FromBody] Availability availability)
        {
            try
            {
                var createdAvailability = await _availabilityService.CreateAvailabilityAsync(Mapper.MapAvailability(psychologistId, availability));
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity creation. Please try again later.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update availability of a psychologist
        /// </summary>
        /// <param name="psychologistId">The psychologist's ID</param>
        /// <param name="availabilityId">The ID of the availability block</param>
        /// <returns>List of availability slots</returns>
        [HttpPut("{psychologistId}/availability/{availabilityId}")]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Availability>> UpdateAvailability([FromRoute] long psychologistId, [FromRoute] long availabilityId, [FromBody] Availability availability)
        {
            try
            {
                var existingAvailability = await _availabilityService.GetAvailabilityAsync(availabilityId);
                if (existingAvailability == null)
                {
                    return await CreateAvailability(psychologistId, availability);
                }
                if (existingAvailability.PsychologistId != psychologistId)
                {
                    return NotFound("Resource with given parameters was not found!");
                }

                var updatedAvailability = await _availabilityService.UpdateAvailabilityAsync(Mapper.MapAvailability(psychologistId, availabilityId, availability));
                return Ok(Mapper.MapAvailability(updatedAvailability));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity update. Please try again later.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
