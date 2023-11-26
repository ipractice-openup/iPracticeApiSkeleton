using System.Collections.Generic;

namespace iPractice.Domain.Models
{
    public class Availability
    {
        public long Id { get; set; }
        public long PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }
}
