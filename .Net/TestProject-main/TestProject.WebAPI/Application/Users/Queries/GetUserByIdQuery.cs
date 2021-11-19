using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<ApiResponse<User>>
    {
        public string UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<ApiResponse<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByIdAsync(request.UserId);
            return new ApiResponse<User>()
            {
                Data = result,
                StatusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                ErrorObject = result == null ? $"User with Id-{request.UserId} not found" : null
            };
        }
    }
}
