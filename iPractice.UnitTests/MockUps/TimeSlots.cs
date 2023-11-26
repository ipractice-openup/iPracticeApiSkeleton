using iPractice.Application.Contract.Dtos;
using System;

namespace iPractice.UnitTests.MockUps
{
    public static class TimeSlots
    {
        public static TimeSlotDto TimeSlot11 = new TimeSlotDto
        {
            Id = 11,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 9, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability1.Id,
            Availability = Availabilities.Availability1
        };
        public static TimeSlotDto TimeSlot12 = new TimeSlotDto
        {
            Id = 12,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 30, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 10, 0, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability1.Id,
            Availability = Availabilities.Availability1
        };
        public static TimeSlotDto TimeSlot13 = new TimeSlotDto
        {
            Id = 13,
            DateTimeFrom = new DateTime(2023, 11, 28, 10, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 10, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability1.Id,
            Availability = Availabilities.Availability1
        };
        public static TimeSlotDto TimeSlot14 = new TimeSlotDto
        {
            Id = 14,
            DateTimeFrom = new DateTime(2023, 11, 28, 10, 30, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 11, 0, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability1.Id,
            Availability = Availabilities.Availability1
        };
        public static TimeSlotDto TimeSlot21 = new TimeSlotDto
        {
            Id = 21,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 9, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability2.Id,
            Availability = Availabilities.Availability2
        };
        public static TimeSlotDto TimeSlot22 = new TimeSlotDto
        {
            Id = 22,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 30, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 10, 0, 0),
            ClientId = 2,
            AvailabilityId = Availabilities.Availability2.Id,
            Availability = Availabilities.Availability2
        };
        public static TimeSlotDto TimeSlot23 = new TimeSlotDto
        {
            Id = 23,
            DateTimeFrom = new DateTime(2023, 11, 28, 10, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 10, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability2.Id,
            Availability = Availabilities.Availability2
        };
        public static TimeSlotDto TimeSlot24 = new TimeSlotDto
        {
            Id = 24,
            DateTimeFrom = new DateTime(2023, 11, 28, 10, 30, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 11, 0, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability2.Id,
            Availability = Availabilities.Availability2
        };
        public static TimeSlotDto TimeSlot31 = new TimeSlotDto
        {
            Id = 31,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 9, 30, 0),
            ClientId = 3,
            AvailabilityId = Availabilities.Availability3.Id,
            Availability = Availabilities.Availability3
        };
        public static TimeSlotDto TimeSlot41 = new TimeSlotDto
        {
            Id = 41,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 9, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability4.Id,
            Availability = Availabilities.Availability4
        };
        public static TimeSlotDto TimeSlot51 = new TimeSlotDto
        {
            Id = 51,
            DateTimeFrom = new DateTime(2023, 11, 28, 9, 0, 0),
            DateTimeTo = new DateTime(2023, 11, 28, 9, 30, 0),
            ClientId = null,
            AvailabilityId = Availabilities.Availability5.Id,
            Availability = Availabilities.Availability5
        };
    }
}
