using System.Collections.Generic;

namespace iPractice.Application.Contract.Dtos
{
    public class ClientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<PsychologistDto> Psychologists { get; set; }
    }
}