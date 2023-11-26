using iPractice.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Helpers
{
    public static class Mapper
    {
        public static Availability MapAvailability(long psychologistId, Models.Availability availability)
        {
            if (availability == null || availability.TimeSlots?.Any() != true)
            {
                return null;
            }

            return new Availability
            {
                PsychologistId = psychologistId,
                TimeSlots = availability.TimeSlots.ConvertAll(x => new TimeSlot { Id = x.Id, DateTimeFrom = x.DateTimeFrom, DateTimeTo = x.DateTimeTo, ClientId = x.ClientId })
            };
        }

        public static Availability MapAvailability(long psychologistId, long availabilityId, Models.Availability availability)
        {
            if (availability == null || availability.TimeSlots?.Any() != true)
            {
                return null;
            }

            return new Availability
            {
                Id = availabilityId,
                PsychologistId = psychologistId,
                TimeSlots = availability.TimeSlots.ConvertAll(x => new TimeSlot { Id = x.Id, AvailabilityId = availabilityId, DateTimeFrom = x.DateTimeFrom, DateTimeTo = x.DateTimeTo, ClientId = x.ClientId })
            };
        }

        public static Models.Availability MapAvailability(Availability availability)
        {
            if (availability == null)
            {
                return null;
            }

            return new Models.Availability
            {
                Id = availability.Id,
                TimeSlots = availability.TimeSlots.ConvertAll(x => new Models.TimeSlot { Id = x.Id, DateTimeFrom = x.DateTimeFrom, DateTimeTo = x.DateTimeTo, ClientId = x.ClientId }),
            };
        }

        public static IEnumerable<Models.TimeSlot> MapTimeSlots(IEnumerable<Availability> availabilities)
        {
            if (availabilities?.Any() != true)
            {
                return Enumerable.Empty<Models.TimeSlot>();
            }

            return availabilities.SelectMany(x => x.TimeSlots).Select(x => new Models.TimeSlot
            {
                Id = x.Id,
                ClientId = x.ClientId,
                DateTimeFrom = x.DateTimeFrom,
                DateTimeTo = x.DateTimeTo,
            });
        }
    }
}
