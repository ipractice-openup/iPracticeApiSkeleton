using System;

namespace iPractice.Domain.Exceptions
{
    public class ClientNotFoundException : NotFoundException
    {
        public ClientNotFoundException()
        { }

        public ClientNotFoundException(long id)
            : base($"Couldn't find client with id {id}")
        { }

        public ClientNotFoundException(string message)
            : base(message)
        { }

        public ClientNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
