using iPractice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Domain.Services
{
    public interface IAppointmentsService
    {
        Task<Appointment> CreateAsync(Appointment appointment);
    }
}
