using System;

namespace RequestViewer.EntityFramework.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsApproved { get; set; }
        public DateTime AllowedDate { get; set; }

        public int PeriodId { get; set; }
        public PeriodDto Period { get; set; }
    }
}
