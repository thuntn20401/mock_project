using InsternShip.Data.ViewModels.Itrsinterview;

namespace InsternShip.Data.ViewModels.Interview
{
    public class InterviewUpdateModel
    {
        public Guid InterviewId { get; set; }
        public Guid RecruiterId { get; set; }
        public Guid InterviewerId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid? ItrsinterviewId { get; set; } = null!;
        public virtual ItrsinterviewAddModel? Itrsinterview { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }

        public string? Priority { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}