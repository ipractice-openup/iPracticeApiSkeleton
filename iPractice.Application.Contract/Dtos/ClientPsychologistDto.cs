namespace iPractice.Application.Contract.Dtos
{
    public class ClientPsychologistDto
    {
        public long ClientId { get; set; }
        public long PsychologistId { get; set; }

        public ClientDto Client { get; set; }
        public PsychologistDto Psychologist { get; set; }
    }
}
