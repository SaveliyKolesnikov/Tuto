using AutoMapper;
using Tuto.Domain.Authorization;
using Tuto.Domain.Models;

namespace Tuto.API.Mapping.MappingExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfo, User>();
        }
    }
}
