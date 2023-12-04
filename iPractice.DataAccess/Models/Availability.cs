using System;

namespace iPractice.DataAccess.Models
{
    public class Availability
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Psychologist Psychologist { get; set; }
    }
}