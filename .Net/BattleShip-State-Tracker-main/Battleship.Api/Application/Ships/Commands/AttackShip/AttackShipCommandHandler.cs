using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Common;
using Battleship.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.Api.Application.Ships.Commands.AttackShip
{
    public class AttackShipCommandHandler : IRequestHandler<AttackShipCommand, AttackViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public AttackShipCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AttackViewModel> Handle(AttackShipCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(request.BoardId);
            if (board == null)
            {
                throw new NotFoundException(nameof(Board), request.BoardId);
            }

            var part = await _context.ShipParts
                .Where(x => x.Ship.Board == board)
                .Where(x => x.X == request.AttackX && x.Y == request.AttackY)
                .FirstOrDefaultAsync();

            if (part == null)
            {
                // Miss
                return new AttackViewModel()
                {
                    X = request.AttackX,
                    Y = request.AttackY
                };
            }

            if (!part.Hit)
            {
                // Hit
                part.Hit = true;
                await _context.SaveChangesAsync(cancellationToken);
            }

            var viewModel = _mapper.Map<AttackViewModel>(part);

            return viewModel;
        }
    }
}
