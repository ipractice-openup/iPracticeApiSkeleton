using System;

namespace iPractice.Api.Models
{
    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long PsychologistId { get; set; }
    }
}