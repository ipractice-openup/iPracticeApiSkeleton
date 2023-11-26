using iPractice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Services
{
    public interface IAvailabilityService
    {
        Task<Availability> GetAvailabilityAsync(long availabilityId);
        Task<Availability> CreateAvailabilityAsync(Availability availability);
        Task<Availability> UpdateAvailabilityAsync(Availability availability);
        Task<IEnumerable<Availability>> GetAvailableTimeSlotsAsync(long clientId);
    }
}
