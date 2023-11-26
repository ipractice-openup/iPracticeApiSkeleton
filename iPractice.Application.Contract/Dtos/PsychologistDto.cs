using System.Collections.Generic;

namespace iPractice.Application.Contract.Dtos
{
    public class PsychologistDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ClientDto> Clients { get; set; }
        public List<AvailabilityDto> Availabilities { get; set; }
    }
}