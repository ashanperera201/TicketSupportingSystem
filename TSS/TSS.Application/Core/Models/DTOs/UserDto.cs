#region References
#endregion

#region Namespace
namespace TSS.Application.Core.Models.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public RoleDto Role { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
#endregion