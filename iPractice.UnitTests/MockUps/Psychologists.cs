using iPractice.Application.Contract.Dtos;

namespace iPractice.UnitTests.MockUps
{
    public static class Psychologists
    {
        public static PsychologistDto Psychologist1 = new PsychologistDto
        {
            Id = 1,
            Name = "Psychologist1",
            Availabilities = new System.Collections.Generic.List<AvailabilityDto> { Availabilities.Availability1 },
            Clients = new System.Collections.Generic.List<ClientDto> { Clients.Client1, Clients.Client3 }
        };
        public static PsychologistDto Psychologist2 = new PsychologistDto
        {
            Id = 2,
            Name = "Psychologist2",
            Availabilities = new System.Collections.Generic.List<AvailabilityDto> { Availabilities.Availability2 },
            Clients = new System.Collections.Generic.List<ClientDto> { Clients.Client2, Clients.Client4 }
        };
        public static PsychologistDto Psychologist3 = new PsychologistDto
        {
            Id = 3,
            Name = "",
            Availabilities = new System.Collections.Generic.List<AvailabilityDto> { Availabilities.Availability3 },
            Clients = new System.Collections.Generic.List<ClientDto> { Clients.Client3, Clients.Client2 }
        };
        public static PsychologistDto Psychologist4 = new PsychologistDto
        {
            Id = 4,
            Name = null,
            Availabilities = new System.Collections.Generic.List<AvailabilityDto> { Availabilities.Availability4, Availabilities.Availability5 },
            Clients = new System.Collections.Generic.List<ClientDto> { Clients.Client4, Clients.Client1 }
        };
    }
}
