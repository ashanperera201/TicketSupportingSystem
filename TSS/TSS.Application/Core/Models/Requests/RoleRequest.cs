namespace TSS.Application.Core.Models.Requests
{
    public class RoleRequest
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
