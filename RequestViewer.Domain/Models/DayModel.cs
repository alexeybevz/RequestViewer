namespace RequestViewer.Domain.Models
{
    public class DayModel
    {
        public string Day { get; set; }
        public bool IsHeader { get; set; }
        public bool IsOpen { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCanEdit { get; set; }

        public DayModel(bool isCanEdit)
        {
            IsCanEdit = isCanEdit;
        }
    }
}