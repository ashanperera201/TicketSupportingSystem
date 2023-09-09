#region References
using System.ComponentModel.DataAnnotations;
using TSS.Domain.Enums;
using TSS.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

#region Namespace
namespace TSS.Domain.Entities
{
    public class Projects : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid ProjectAssignedUser { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public UserStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Users? User { get; set; }
        public ICollection<Tickets> Tickets { get; set; }

    }
}
#endregion