using AutoMapper;
using iPractice.Application.Contract.Dtos;
using iPractice.Domain.Models;
using iPractice.Interfaces.DataAccess;
using iPractice.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPractice.Application.Services
{
    public class AvailabilityService : BaseService, IAvailabilityService
    {
        private readonly IPsychologistService _psychologistService;

        public AvailabilityService(IUnitOfWork unitOfWork, IMapper mapper, IPsychologistService psychologistService) : base(unitOfWork, mapper)
        {
            _psychologistService = psychologistService;
        }

        public async Task<Availability> GetAvailabilityAsync(long availabilityId)
        {
            var availabilityDto = await _unitOfWork.Availability.FindAsync(availabilityId);
            if (availabilityDto == null)
            {
                return null;
            }
            return _mapper.Map<Availability>(availabilityDto);
        }

        public async Task<Availability> CreateAvailabilityAsync(Availability availability)
        {
            if (availability == null)
            {
                throw new ArgumentNullException(nameof(availability));
            }

            var availabilityDto = _mapper.Map<AvailabilityDto>(availability);
            availabilityDto = await _unitOfWork.Availability.CreateAsync(availabilityDto);
            return _mapper.Map<Availability>(availabilityDto);
        }

        public async Task<Availability> UpdateAvailabilityAsync(Availability availability)
        {
            if (availability == null)
            {
                throw new ArgumentNullException(nameof(availability));
            }

            var availabilityDto = _mapper.Map<AvailabilityDto>(availability);
            var timeSlotsDto = await _unitOfWork.TimeSlot.UpdateAsync(availabilityDto.TimeSlots);
            availabilityDto.TimeSlots = timeSlotsDto.ToList();
            return _mapper.Map<Availability>(availabilityDto);
        }

        public async Task<IEnumerable<Availability>> GetAvailableTimeSlotsAsync(long clientId)
        {
            var psychologists = await _psychologistService.GetPsychologistsAsync(clientId);
            if (psychologists == null)
            {
                return Enumerable.Empty<Availability>();
            }

            var availabilitiesDto = _unitOfWork.Availability.GetByIds(psychologists.Select(x => x.Id));
            if (availabilitiesDto == null)
            {
                return Enumerable.Empty<Availability>();
            }

            foreach (var availability in availabilitiesDto)
            {
                availability.TimeSlots = availability.TimeSlots.Where(x => !x.ClientId.HasValue || x.ClientId == 0).ToList();
            }

            return _mapper.Map<IEnumerable<Availability>>(availabilitiesDto);
        }
    }
}
