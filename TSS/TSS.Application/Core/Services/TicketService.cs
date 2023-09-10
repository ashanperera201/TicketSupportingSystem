#region References
using Microsoft.AspNetCore.Http;
using TSS.Application.Core.Models.DTOs;
using TSS.Application.Core.Models.Requests;
using TSS.Application.Interfaces;
using TSS.Application.Models;
using TSS.Domain.Core.Repositories;
using TSS.Domain.Entities;
using TSS.Domain.Enums;
#endregion

#region Namespace

namespace TSS.Application.Core.Services
{
    public class TicketService : ITicketService
    {
        /// <summary>
        /// The ticket repository
        /// </summary>
        private readonly ITicketRepository _ticketRepository;
        /// <summary>
        /// The entity mapper service
        /// </summary>
        private readonly IEntityMapperService _entityMapperService;
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="ticketRepository">The ticket repository.</param>
        /// <param name="entityMapperService">The entity mapper service.</param>
        public TicketService(ITicketRepository ticketRepository, IEntityMapperService entityMapperService)
        {
            _ticketRepository = ticketRepository;
            _entityMapperService = entityMapperService;
        }

        /// <summary>
        /// Gets all tickets asynchronous.
        /// </summary>
        /// <param name="filterRequest">The filter request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public GenericResponse<List<TicketDto>> GetAllTickets(FilterRequestDto filterRequest)
        {
            // CHECK IS ADMIN AND THE CLIENT LATER.
            var tickets = _ticketRepository.GetTicketsQuery().Where(x =>
                          x.IsDeleted == filterRequest.IsDeleted &&
                          x.Status == (UserStatus)filterRequest.Status! &&
                          x.Project.ProjectAssignedUser == Guid.Parse(filterRequest.UserId)).ToList();

            var mappedToDto = _entityMapperService.Map<List<Tickets>, List<TicketDto>>(tickets);

            return new GenericResponse<List<TicketDto>>
            {
                IsSuccess = true,
                Response = mappedToDto,
                Status = 200
            };
        }

        /// <summary>
        /// Gets the ticket.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns></returns>
        public TicketDto? GetTicket(string ticketId)
        {
            var ticket = _ticketRepository.GetTicketsQuery().FirstOrDefault(x => x.Id == Guid.Parse(ticketId));
            if (ticket != null)
            {
                return _entityMapperService.Map<Tickets, TicketDto>(ticket);
            }
            return null;
        }

        /// <summary>
        /// Saves the ticket asynchronous.
        /// </summary>
        /// <param name="ticket">The ticket dto.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GenericResponse<TicketDto>> SaveTicketAsync(TicketRequest ticket, CancellationToken cancellationToken = default)
        {
            var response = new GenericResponse<TicketDto>();

            var mappedTicket = _entityMapperService.Map<TicketRequest, Tickets>(ticket);
            var savedRes = await _ticketRepository.SaveTicketsAsync(mappedTicket, cancellationToken);

            if (savedRes != null)
            {
                response.Response = _entityMapperService.Map<Tickets, TicketDto>(savedRes);
                response.IsSuccess = true;
                response.Status = StatusCodes.Status200OK;
            }

            response.Status = StatusCodes.Status417ExpectationFailed;

            return response;
        }

        /// <summary>
        /// Updates the ticket asynchronous.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <param name="ticket">The ticket.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TicketDto?> UpdateTicketAsync(string ticketId, TicketRequest ticket, CancellationToken cancellationToken = default)
        {
            var databaseTicket = await _ticketRepository.GetTicketByIdAsync(Guid.Parse(ticketId), cancellationToken);
            if (databaseTicket != null)
            {
                databaseTicket.TicketName = ticket.TicketName;
                databaseTicket.TicketCode = ticket.TicketCode;

                var updateRes = await _ticketRepository.UpdateTicketsAsync(databaseTicket, cancellationToken);
                return _entityMapperService.Map<Tickets, TicketDto>(updateRes);
            }
            return null;
        }
    }
}
#endregion