using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.Api.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GameViewModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = new Game();

            _context.Games.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<GameViewModel>(entity);

            return viewModel; ;
        }
    }
}
