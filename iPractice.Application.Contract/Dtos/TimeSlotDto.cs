using System;

namespace iPractice.Application.Contract.Dtos
{
    public class TimeSlotDto
    {
        public long Id { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public long? ClientId { get; set; }
        public long AvailabilityId { get; set; }
        public AvailabilityDto Availability { get; set; }
    }
}
