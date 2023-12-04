using iPractice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Domain.Services
{
    public interface IAvailabilityService
    {
        Task<Availability> CreateAsync(Availability availability);
        Task<IEnumerable<Availability>> GetAsync(long clientId);
    }
}
