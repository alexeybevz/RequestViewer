using System;

namespace RequestViewer.Domain.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public Guid RequestId { get; set; }
    }
}
