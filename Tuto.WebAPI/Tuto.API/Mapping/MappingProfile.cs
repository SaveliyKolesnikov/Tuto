using AutoMapper;
using Tuto.Domain.Authorization;
using Tuto.Domain.Models;

namespace Tuto.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfo, User>();
        }
    }
}
