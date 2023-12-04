using System;

namespace iPractice.Domain.Exceptions
{
    public class PsychologistNotFoundException : NotFoundException
    {
        public PsychologistNotFoundException()
        { }

        public PsychologistNotFoundException(long id)
            : base($"Couldn't find psychologyst with id {id}")
        { }

        public PsychologistNotFoundException(string message)
            : base(message)
        { }

        public PsychologistNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
