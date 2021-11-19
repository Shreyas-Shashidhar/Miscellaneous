using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using TestProject.WebAPI.Infrastructure.Data;

namespace TestProject.WebAPI.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public AccountRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }

        public async Task<Account> CreateAccountEntryAsync(string userId)
        {
            var result = await _testProjectDbContext.Accounts.AddAsync(new Account() { UserId = userId });
            await _testProjectDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _testProjectDbContext.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsByUserIdAsync(string userId)
        {
            return await _testProjectDbContext.Accounts.Where(item => item.UserId.Equals(userId)).ToListAsync();
        }
    }
}
