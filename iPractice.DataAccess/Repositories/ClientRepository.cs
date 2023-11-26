using iPractice.Application.Contract.Dtos;
using iPractice.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.DataAccess.Repositories
{
    public class PsychologistRepository : GenericRepository<PsychologistDto>, IPsychologistRepository
    {
        public PsychologistRepository(ApplicationDbContext dbContext, ILogger<PsychologistDto> logger) : base(dbContext, logger)
        {
        }

        public async Task<IEnumerable<PsychologistDto>> GetPsychologists(long clientId)
        {
            return (await DbContext.Clients.Include(x => x.Psychologists).FirstOrDefaultAsync(x => x.Id == clientId))?.Psychologists;
        }
    }
}
