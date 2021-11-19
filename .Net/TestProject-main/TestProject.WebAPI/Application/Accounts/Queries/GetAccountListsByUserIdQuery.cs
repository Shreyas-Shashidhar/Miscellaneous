using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Interfaces;

namespace TestProject.WebAPI.Application.Accounts.Queries
{
    public class GetAccountListsByUserIdQuery : IRequest<ApiResponse<IEnumerable<AccountDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetAccountListsByUserIdQueryHandler : IRequestHandler<GetAccountListsByUserIdQuery, ApiResponse<IEnumerable<AccountDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAccountListsByUserIdQueryHandler(IAccountRepository accountRepo, IUserRepository userRepository, IMapper mapper)
        {
            _accountRepository = accountRepo;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<ApiResponse<IEnumerable<AccountDto>>> Handle(GetAccountListsByUserIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user == null)
            {
                return new ApiResponse<IEnumerable<AccountDto>>() { ErrorObject = $"User with Id:{request.UserId} not found", StatusCode = HttpStatusCode.NotFound };
            }
            var accounts = await _accountRepository.GetAllAccountsByUserIdAsync(request.UserId);

            if (accounts == null || accounts.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<AccountDto>>()
                {
                    ErrorObject = $"No Accounts Found for User with Id:{request.UserId}",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var result = accounts.Select(item =>
             {
                 var accountDto = _mapper.Map<AccountDto>(item);
                 accountDto.UserName = user.Name;
                 accountDto.UserEmailAddress = user.EmailAddress;
                 return accountDto;
             });

            return new ApiResponse<IEnumerable<AccountDto>>()
            {
                Data = result,
                StatusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                ErrorObject = result == null ? $"User with Id:{request.UserId} not found" : null
            };
        }
    }

}

