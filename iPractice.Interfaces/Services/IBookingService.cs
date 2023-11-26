using iPractice.Domain.Models;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Services
{
    public interface IBookingService
    {
        Task<TimeSlot> GetTimeSlotAsync(long timeSlotId);
        Task CreateAppointmentAsync(long clientId, long timeSlotId);
    }
}
