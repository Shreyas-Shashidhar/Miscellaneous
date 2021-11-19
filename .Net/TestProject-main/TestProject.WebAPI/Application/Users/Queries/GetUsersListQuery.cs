using MediatR;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Application.Users.Queries
{
    public class GetUsersListQuery : IRequest<ApiResponse<IEnumerable<User>>>
    {
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, ApiResponse<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersListQueryHandler(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<ApiResponse<IEnumerable<User>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllUsersAsync();
            return new ApiResponse<IEnumerable<User>>() { StatusCode = HttpStatusCode.OK, Data = result };
        }
    }
}
