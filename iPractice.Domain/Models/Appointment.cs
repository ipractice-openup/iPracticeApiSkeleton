using iPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iPractice.Domain.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long PsychologistId { get; set; }
        public long ClientId { get; set; }
    }
}
