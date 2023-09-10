#region References
using TSS.Application.Core.Models.DTOs;
#endregion

#region Namespace
namespace TSS.Application.Core.Models.Requests
{
    public class TicketRequest
    {
        public Guid? ProjectId { get; set; }
        public string TicketName { get; set; }
        public string TicketCode { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public ProjectDto? Project { get; set; }
    }
}
#endregion