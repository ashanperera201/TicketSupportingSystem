#region References
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

#region Namespace
namespace TSS.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        public CustomerController()
        {
        }

        [HttpGet]
        public Task<IActionResult> GetAllCustomersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public Task<IActionResult> SaveCustomersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        [HttpPut()]
        public Task<IActionResult> UpdateCustomersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete()]
        public Task<IActionResult> DeleteCustomersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion