using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountEntryAsync(string UserId);

        Task<IEnumerable<Account>> GetAllAccountsByUserIdAsync(string userId);

        Task<IEnumerable<Account>> GetAllAccountsAsync();

    }
}
