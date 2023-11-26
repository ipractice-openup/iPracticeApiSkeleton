﻿using System.Collections.Generic;

namespace iPractice.Domain.Models
{
    public class Psychologist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; }
        public List<Availability> Availabilities { get; set; }
    }
}
