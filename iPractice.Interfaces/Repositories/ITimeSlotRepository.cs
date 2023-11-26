using iPractice.Application.Contract.Dtos;
using iPractice.Domain.Models;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Repositories
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlotDto>
    {
        Task UpdateAsync(TimeSlot timeSlot);
    }
}
