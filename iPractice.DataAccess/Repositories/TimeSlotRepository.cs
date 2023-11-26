using iPractice.Application.Contract.Dtos;
using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using iPractice.Domain.Models;

namespace iPractice.DataAccess.Repositories
{
    public class TimeSlotRepository : GenericRepository<TimeSlotDto>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext dbContext, ILogger<TimeSlotDto> logger) : base(dbContext, logger)
        {
        }

        public async Task UpdateAsync(TimeSlot timeSlot)
        {
            if (timeSlot == null)
            {
                return;
            }
            try
            {
                var entity = await DbContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == timeSlot.Id);
                entity.DateTimeFrom = timeSlot.DateTimeFrom;
                entity.DateTimeTo = timeSlot.DateTimeTo;
                entity.ClientId = timeSlot.ClientId;
                DbContext.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurs during entity update.");
                throw;
            }

        }
    }
}
