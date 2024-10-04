using System.Collections.Generic;

namespace RequestViewer.EntityFramework.DTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsApproved { get; set; }

        public int PeriodId { get; set; }
        public PeriodDto Period { get; set; }

        public ICollection<RequestDayDto> RequestsDays { get; set; }
    }
}
