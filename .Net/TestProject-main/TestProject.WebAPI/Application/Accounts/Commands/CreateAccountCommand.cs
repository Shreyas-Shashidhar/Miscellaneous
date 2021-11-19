using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Interfaces;

namespace TestProject.WebAPI.Application.Accounts.Commands
{

    public class CreateAccountCommand : IRequest<ApiResponse<AccountDto>>
    {
        public string UserId { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ApiResponse<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AccountDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId);

                if (user == null)
                {
                    return new ApiResponse<AccountDto>() { ErrorObject = $"User with Id:{request.UserId} not found", StatusCode = HttpStatusCode.NotFound };
                }

                var result = await _accountRepository.CreateAccountEntryAsync(request.UserId);
                var data = _mapper.Map<AccountDto>(result);

                if (data != null)
                {
                    data.UserName = user.Name;
                    data.UserEmailAddress = user.EmailAddress;
                }

                return new ApiResponse<AccountDto>()
                {
                    Data = data,
                    StatusCode = data != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                    ErrorObject = data == null ? $"Failed to create account" : null
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<AccountDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorObject = $"{ex.Message}"
                };
            }
        }
    }
}
