#region Namespace
namespace TSS.Application.Core.Models.DTOs
{
    public class TicketDto
    {
        public Guid? Id { get; set; }
        public Guid? ProjectId { get; set; }
        public string? TicketName { get; set; }
        public string? TicketCode { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
#endregion