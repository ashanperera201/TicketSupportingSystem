#region References
using System.ComponentModel.DataAnnotations;
#endregion

#region Namespace
namespace TSS.Application.Core.Models.Requests
{
    public class ProjectRequest
    {
        [Required]
        public string projectAssignedUser { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectCode { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
#endregion