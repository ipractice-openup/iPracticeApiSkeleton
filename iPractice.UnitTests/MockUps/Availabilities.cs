using iPractice.Application.Contract.Dtos;

namespace iPractice.UnitTests.MockUps
{
    public static class Availabilities
    {
        public static AvailabilityDto Availability1 = new AvailabilityDto
        {
            Id = 1,
            PsychologistId = Psychologists.Psychologist1.Id,
            Psychologist = Psychologists.Psychologist1,
            TimeSlots = new System.Collections.Generic.List<TimeSlotDto> { TimeSlots.TimeSlot11, TimeSlots.TimeSlot12, TimeSlots.TimeSlot13, TimeSlots.TimeSlot14 }
        };
        public static AvailabilityDto Availability2 = new AvailabilityDto
        {
            Id = 2,
            PsychologistId = Psychologists.Psychologist2.Id,
            Psychologist = Psychologists.Psychologist2,
            TimeSlots = new System.Collections.Generic.List<TimeSlotDto> { TimeSlots.TimeSlot21, TimeSlots.TimeSlot22, TimeSlots.TimeSlot23, TimeSlots.TimeSlot24 }
        };
        public static AvailabilityDto Availability3 = new AvailabilityDto
        {
            Id = 3,
            PsychologistId = Psychologists.Psychologist3.Id,
            Psychologist = Psychologists.Psychologist3,
            TimeSlots = new System.Collections.Generic.List<TimeSlotDto> { TimeSlots.TimeSlot31 }
        };
        public static AvailabilityDto Availability4 = new AvailabilityDto
        {
            Id = 4,
            PsychologistId = Psychologists.Psychologist4.Id,
            Psychologist = Psychologists.Psychologist4,
            TimeSlots = new System.Collections.Generic.List<TimeSlotDto> { TimeSlots.TimeSlot41 }
        };
        public static AvailabilityDto Availability5 = new AvailabilityDto
        {
            Id = 5,
            PsychologistId = Psychologists.Psychologist4.Id,
            Psychologist = Psychologists.Psychologist4,
            TimeSlots = new System.Collections.Generic.List<TimeSlotDto> { TimeSlots.TimeSlot51 }
        };
    }
}
