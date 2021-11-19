using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using TestProject.WebAPI.Infrastructure.Data;

namespace TestProject.WebAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public UserRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }
        public async Task<User> CreateUserEntryAsync(User user)
        {
            var result = await _testProjectDbContext.Users.AddAsync(user);
            await _testProjectDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _testProjectDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _testProjectDbContext.Users.FindAsync(userId);
        }
    }
}
