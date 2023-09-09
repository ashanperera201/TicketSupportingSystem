using System.ComponentModel.DataAnnotations;

namespace TSS.Application.Core.Models.DTOs;
public class ProjectDto
{
    public Guid? Id { get; set; }
    public Guid? ProjectAssignedUser { get; set; }
    [Required]
    public string ProjectName { get; set; }
    [Required]
    public string ProjectCode { get; set; }
    public int? Status { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public UserDto? User { get; set; }
    public List<TicketsDto>? Tickets { get; set; }
}
