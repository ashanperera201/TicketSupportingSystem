#region Namespace
namespace TSS.Application.Models
{
    public class GenericResponse<T> where T : class
    {
        public T Response { get; set; }
        public bool IsSuccess { get; set; } = false;
        public int Status { get; set; }
    }
}
#endregion