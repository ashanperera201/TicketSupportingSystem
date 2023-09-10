#region References
#endregion

#region Namespace
namespace TSS.Application.Core.Models.Requests
{
    public class FilterRequestDto
    {
        public string? CustomerId { get; set; }
        public string? ProjectId { get; set; }
        public string UserId { get; set; }
        public int? Status { get; set; } = 1;
        public bool? IsDeleted { get; set; }
    }
}
#endregion