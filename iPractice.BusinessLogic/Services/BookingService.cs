using AutoMapper;
using iPractice.Domain.Models;
using iPractice.Interfaces.DataAccess;
using iPractice.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Application.Services
{
    public class BookingService : BaseService, IBookingService
    {
        public BookingService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task<TimeSlot> GetTimeSlotAsync(long timeSlotId)
        {
            var timeSlotDto = await _unitOfWork.TimeSlot.FindAsync(timeSlotId);
            if (timeSlotDto == null)
            {
                return null;
            }

            return _mapper.Map<TimeSlot>(timeSlotDto);
        }

        public async Task CreateAppointmentAsync(long clientId, long timeSlotId)
        {
            var timeSlotDto = await _unitOfWork.TimeSlot.FindAsync(timeSlotId);
            if (timeSlotDto == null)
            {
                throw new KeyNotFoundException(nameof(timeSlotDto));
            }

            timeSlotDto.ClientId = clientId;
            var timeSlot = _mapper.Map<TimeSlot>(timeSlotDto);
            await _unitOfWork.TimeSlot.UpdateAsync(timeSlot);
        }
    }
}
