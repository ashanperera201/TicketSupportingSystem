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
    [Route("api/v{version:apiVersion}/tickets")]
    [ApiController]
    public class TicketsController: Controller
    {
        public TicketsController()
        {
        }

        [HttpGet]
        public Task<IActionResult> GetAllTicketsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost()]
        public Task<IActionResult> SaveTicketAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        [HttpPut()]
        public Task<IActionResult> UpdateTicketAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete()]
        public Task<IActionResult> DeleteTicketAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion