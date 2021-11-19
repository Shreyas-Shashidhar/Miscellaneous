using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<ApiResponse<User>>
    {
        public User User { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<ApiResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.User.MontlySalary < 1000)
                {
                    return new ApiResponse<User>() { ErrorObject = "Monthly Salary insufficient", StatusCode = HttpStatusCode.BadRequest };
                }
                var result = await _userRepository.CreateUserEntryAsync(request.User);
                return new ApiResponse<User>()
                {
                    Data = result,
                    StatusCode = result != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                    ErrorObject = result == null ? $"Failed to create user" : null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorObject = $"{ex}"
                };
            }
        }
    }
}
