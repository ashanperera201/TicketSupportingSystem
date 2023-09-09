#region References
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSS.Domain.Core.Models;
using TSS.Domain.Enums;
#endregion

#region Namespace
namespace TSS.Domain.Entities
{
    public class Users : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Role")]
        public Guid? RoleId { get; set; }
        public Guid? ProjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Roles? Role { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
#endregion