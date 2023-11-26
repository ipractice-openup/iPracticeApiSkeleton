using AutoMapper;
using iPractice.Domain.Models;
using iPractice.Interfaces.DataAccess;
using iPractice.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Application.Services
{
    public class PsychologistService : BaseService, IPsychologistService
    {
        public PsychologistService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        { }

        public async Task<IEnumerable<Psychologist>> GetPsychologistsAsync(long clientId)
        {
            var psychologistsDto = await _unitOfWork.Psychologist.GetPsychologists(clientId);
            return _mapper.Map<IEnumerable<Psychologist>>(psychologistsDto);
        }
    }
}
