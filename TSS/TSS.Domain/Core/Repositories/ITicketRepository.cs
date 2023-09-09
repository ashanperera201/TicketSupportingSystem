#region References
using TSS.Domain.Entities;
#endregion

#region Namespace
namespace TSS.Domain.Core.Repositories
{
    public interface ITicketRepository
    {
        /// <summary>
        /// Saves the tickets asynchronous.
        /// </summary>
        /// <param name="tickets">The tickets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Tickets> SaveTicketsAsync(Tickets tickets, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the tickets asynchronous.
        /// </summary>
        /// <param name="tickets">The tickets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Tickets> UpdateTicketsAsync(Tickets tickets, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the ticket by identifier asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<Tickets?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes the ticket by identifier asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<bool> DeleteTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
    }
}
#endregion