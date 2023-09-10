#region References
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Interfaces;
#endregion

#region Namespace
namespace TSS.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tickets")]
    [ApiController]
    public class TicketsController : Controller
    {
        /// <summary>
        /// The ticket service
        /// </summary>
        private readonly ITicketService _ticketService;
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketsController" /> class.
        /// </summary>
        /// <param name="ticketService">The ticket service.</param>
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Gets all tickets asynchronous.
        /// </summary>
        /// <param name="filterRequest">The filter request.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTicketsAsync([FromQuery] FilterRequestDto filterRequest)
        {
            try
            {
                var tickets = _ticketService.GetAllTickets(filterRequest);
                return StatusCode(tickets.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError, tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the ticket by identifier.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns></returns>
        [HttpGet("id/{ticketId}")]
        public IActionResult GetTicketById(string ticketId)
        {
            try
            {
                var ticket = _ticketService.GetTicket(ticketId);
                return StatusCode(ticket !=null ? StatusCodes.Status200OK : StatusCodes.Status204NoContent, ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Saves the ticket asynchronous.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> SaveTicketAsync([FromBody] TicketRequest ticket, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var savedRes = await _ticketService.SaveTicketAsync(ticket, cancellationToken);
                    return StatusCode(savedRes != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, savedRes);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, JsonConvert.SerializeObject(ModelState.Values.Select(x => x.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the ticket asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="ticketRequest">The ticket request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPut("id/{ticketId}")]
        public async Task<IActionResult> UpdateTicketAsync(string ticketId, TicketRequest ticketRequest ,CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedRes = await _ticketService.UpdateTicketAsync(ticketId, ticketRequest, cancellationToken);
                    return StatusCode(updatedRes != null ? StatusCodes.Status200OK : StatusCodes.Status417ExpectationFailed, updatedRes);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, JsonConvert.SerializeObject(ModelState.Values.Select(x => x.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete()]
        public Task<IActionResult> DeleteTicketAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion