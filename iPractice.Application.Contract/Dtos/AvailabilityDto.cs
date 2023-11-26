using System.Collections.Generic;

namespace iPractice.Application.Contract.Dtos
{
    public class AvailabilityDto
    {
        public long Id { get; set; }
        public long PsychologistId { get; set; }
        public PsychologistDto Psychologist { get; set; }
        public List<TimeSlotDto> TimeSlots { get; set; }
    }
}
