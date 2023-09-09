#region References
using Microsoft.EntityFrameworkCore;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
using TSS.Infrastructure.Data;
#endregion

#region Namespace
namespace TSS.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        /// <summary>
        /// The TSS database context
        /// </summary>
        private readonly TSSDbContext _tssDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketRepository"/> class.
        /// </summary>
        /// <param name="tssDbContext">The TSS database context.</param>
        public TicketRepository(TSSDbContext tssDbContext)
        {
            _tssDbContext = tssDbContext;
        }

        /// <summary>
        /// Deletes the ticket by identifier asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> DeleteTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await GetTicketByIdAsync(ticketId, cancellationToken);
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                _tssDbContext.Tickets.Remove(ticket);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Gets the ticket by identifier asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Tickets?> GetTicketByIdAsync(Guid ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _tssDbContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId, cancellationToken);
            return ticket;
        }

        /// <summary>
        /// Saves the tickets asynchronous.
        /// </summary>
        /// <param name="tickets">The tickets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Tickets> SaveTicketsAsync(Tickets tickets, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedResult = await _tssDbContext.Tickets.AddAsync(tickets, cancellationToken);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedResult.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Updates the tickets asynchronous.
        /// </summary>
        /// <param name="tickets">The tickets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<Tickets> UpdateTicketsAsync(Tickets tickets, CancellationToken cancellationToken = default)
        {
            var transaction = await _tssDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var savedResult = _tssDbContext.Tickets.Update(tickets);
                await _tssDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return savedResult.Entity;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
#endregion