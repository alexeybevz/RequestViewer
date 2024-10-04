using System;

namespace RequestViewer.Domain.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int RequestId { get; set; }
    }
}
