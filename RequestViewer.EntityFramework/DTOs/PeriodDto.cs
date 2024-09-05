﻿using System;
using System.Collections.Generic;

namespace RequestViewer.EntityFramework.DTOs
{
    public class PeriodDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnabled { get; set; }

        public ICollection<RequestDTO> Requests { get; set; }
    }
}
