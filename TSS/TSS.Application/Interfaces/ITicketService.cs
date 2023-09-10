#region References
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Models;
#endregion

#region Namespace

namespace TSS.Application.Interfaces
{
    public interface ITicketService
    {
        /// <summary>
        /// Gets the ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns></returns>
        TicketDto? GetTicket(string ticketId);
        /// <summary>
        /// Gets all tickets asynchronous.
        /// </summary>
        /// <param name="filterRequest">The filter request.</param>
        /// <returns></returns>
        GenericResponse<List<TicketDto>> GetAllTickets(FilterRequestDto filterRequest);
        /// <summary>
        /// Saves the ticket asynchronous.
        /// </summary>
        /// <param name="ticketDto">The ticket dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<GenericResponse<TicketDto>> SaveTicketAsync(TicketRequest ticketDto, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the ticket asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="ticket">The ticket.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TicketDto?> UpdateTicketAsync(string ticketId, TicketRequest ticket, CancellationToken cancellationToken = default);
    }
}
#endregion