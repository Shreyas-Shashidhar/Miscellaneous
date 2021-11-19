using AutoMapper;
using TestProject.WebAPI.Application.Accounts;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
