using iPractice.Application.Contract.Dtos;
using System.Collections.Generic;

namespace iPractice.Interfaces.Repositories
{
    public interface IAvailabilityRepository : IGenericRepository<AvailabilityDto>
    {
        IEnumerable<AvailabilityDto> GetByIds(IEnumerable<long> psychologistsIds);
    }
}
