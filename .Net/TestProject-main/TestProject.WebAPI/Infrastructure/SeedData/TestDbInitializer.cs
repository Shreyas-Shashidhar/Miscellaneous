using System.Linq;
using TestProject.WebAPI.Domain.Models;
using TestProject.WebAPI.Infrastructure.Data;

namespace TestProject.WebAPI.Infrastructure.SeedData
{
    public static class TestDbInitializer
    {
        public static void Initialize(TestProjectDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any() || context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{Name="Professor",EmailAddress="Professor@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Berlin",EmailAddress="Berlin@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Tokyo",EmailAddress="Tokyo@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Nairobi",EmailAddress="Nairobi@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Rio",EmailAddress="Rio@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Denver",EmailAddress="Denver@test.com",MontlyExpenses=2500, MontlySalary = 7000},
                new User{Name="Helsinki",EmailAddress="Helsinki@test.com",MontlyExpenses=2500, MontlySalary = 7000},

            };
            context.Users.AddRange(users);
            context.SaveChanges();

            foreach (var user in context.Users)
            {
                var accounts = new Account[]
                {
                    new Account() {UserId = user.Id},
                    new Account() {UserId = user.Id},
                    new Account() {UserId = user.Id},
                    new Account() {UserId = user.Id},
                    new Account() {UserId = user.Id},
                };

                context.Accounts.AddRange(accounts);
            }
            context.SaveChanges();

        }
    }
}
