using iPractice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Interfaces.Services
{
    public interface IPsychologistService
    {
        Task<IEnumerable<Psychologist>> GetPsychologistsAsync(long clientId);
    }
}
