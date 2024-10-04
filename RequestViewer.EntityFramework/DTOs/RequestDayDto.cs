using System;

namespace RequestViewer.EntityFramework.DTOs
{
    public class RequestDayDto
    {
        public int Id { get; set; }
        public DateTime AllowedDate { get; set; }

        public int RequestId { get; set; }
        public RequestDto Request { get; set; }
    }
}