using System;

namespace iPractice.Domain.Models
{
    public class TimeSlot
    {
        public long Id {  get; set; }
        public DateTime DateTimeFrom {  get; set; }
        public DateTime DateTimeTo {  get; set; }
        public long? ClientId { get; set; }
        public long AvailabilityId { get; set; }
        public Availability Availability { get; set; }
    }
}
