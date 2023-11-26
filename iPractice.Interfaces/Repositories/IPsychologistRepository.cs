using iPractice.Application.Contract.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Repositories
{
    public interface IPsychologistRepository : IGenericRepository<PsychologistDto>
    {
        Task<IEnumerable<PsychologistDto>> GetPsychologists(long clientId);
    }
}
