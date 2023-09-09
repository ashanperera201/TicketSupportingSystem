#region References
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TSS.Domain.Core.Models;
using TSS.Domain.Enums;
#endregion

#region Namespace
namespace TSS.Domain.Entities
{
    public class Tickets: BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public string TicketName { get; set; }
        public string TicketCode { get; set; }
        public UserStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Projects Project { get; set; }
    }
}
#endregion