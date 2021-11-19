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
    public class GetAccountsListQuery : IRequest<ApiResponse<IEnumerable<AccountDto>>>
    {
    }

    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, ApiResponse<IEnumerable<AccountDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAccountsListQueryHandler(IAccountRepository accountRepo, IUserRepository userRepository, IMapper mapper)
        {
            _accountRepository = accountRepo;
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<ApiResponse<IEnumerable<AccountDto>>> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {

            var accounts = await _accountRepository.GetAllAccountsAsync();
            var users = await _userRepository.GetAllUsersAsync();

            var result = (from user in users
                          join account in accounts on user.Id equals account.UserId
                          select new AccountDto()
                          {
                              Id = account.Id,
                              UserEmailAddress = user.EmailAddress,
                              UserName = user.Name,
                              UserId = user.Id
                          }).ToList();

            return new ApiResponse<IEnumerable<AccountDto>>() { StatusCode = HttpStatusCode.OK, Data = result };
        }
    }
}
