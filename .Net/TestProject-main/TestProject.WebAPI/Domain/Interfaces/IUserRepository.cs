using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserEntryAsync(User user);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(string userId);
    }
}
