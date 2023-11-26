using iPractice.Application.Contract.Dtos;
using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.DataAccess.Repositories
{
    public class AvailabilityRepository : GenericRepository<AvailabilityDto>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext dbContext, ILogger<AvailabilityDto> logger) : base(dbContext, logger)
        {
        }

        public IEnumerable<AvailabilityDto> GetByIds(IEnumerable<long> psychologistsIds)
        {
            if (psychologistsIds?.Any() != true)
            {
                return Enumerable.Empty<AvailabilityDto>();
            }

            return DbContext.Availabilities.Where(x => psychologistsIds.Contains(x.PsychologistId)).Include(x => x.TimeSlots).Include(x => x.Psychologist).ToList();
        }
    }
}
