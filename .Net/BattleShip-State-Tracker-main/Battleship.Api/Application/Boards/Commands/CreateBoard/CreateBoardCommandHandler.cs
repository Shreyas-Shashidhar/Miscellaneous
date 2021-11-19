using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Common;
using Battleship.Api.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.Api.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public CreateBoardCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BoardViewModel> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.GameId);
            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            var entity = new Board
            {
                Game = game,
                DimensionX = request.DimensionX,
                DimensionY = request.DimensionY,
            };

            _context.Boards.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<BoardViewModel>(entity);

            return viewModel;
        }
    }
}
