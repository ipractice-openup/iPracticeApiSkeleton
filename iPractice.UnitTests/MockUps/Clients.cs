using iPractice.Application.Contract.Dtos;

namespace iPractice.UnitTests.MockUps
{
    public static class Clients
    {
        public static ClientDto Client1 = new ClientDto
        {
            Id = 1,
            Name = "Client1",
            Psychologists = new System.Collections.Generic.List<PsychologistDto> { Psychologists.Psychologist1, Psychologists.Psychologist4 }
        };
        public static ClientDto Client2 = new ClientDto
        {
            Id = 2,
            Name = "Client2",
            Psychologists = new System.Collections.Generic.List<PsychologistDto> { Psychologists.Psychologist2, Psychologists.Psychologist3 }
        };
        public static ClientDto Client3 = new ClientDto
        {
            Id = 3,
            Name = "",
            Psychologists = new System.Collections.Generic.List<PsychologistDto> { Psychologists.Psychologist3, Psychologists.Psychologist1 }
        };
        public static ClientDto Client4 = new ClientDto
        {
            Id = 4,
            Name = null,
            Psychologists = new System.Collections.Generic.List<PsychologistDto> { Psychologists.Psychologist4, Psychologists.Psychologist2 }
        };
    }
}
