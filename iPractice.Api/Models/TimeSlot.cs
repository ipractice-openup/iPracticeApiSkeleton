﻿using System;

namespace iPractice.Api.Models
{
    public class TimeSlot
    {
        public long Id { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public long? ClientId { get; set; }
        public bool IsBooked { get { return ClientId.HasValue && ClientId.Value != 0; } }
    }
}
