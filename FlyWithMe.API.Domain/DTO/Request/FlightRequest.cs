﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWithMe.API.Domain.DTO.Request
{
    public class FlightRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public int? AdultCount { get; set; } = 1;
    }
}
