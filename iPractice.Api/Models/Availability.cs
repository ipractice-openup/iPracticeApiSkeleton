﻿using System.Collections.Generic;

namespace iPractice.Api.Models
{
    public class Availability
    {
        public long Id { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }
}