using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tuto.Domain;

namespace Tuto.API.Mapping.MappingExtensions
{
    internal static class MappingExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) =>
            services.AddAutoMapper(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            }, Assembly.GetAssembly(typeof(ApplicationDbContext)));
    }
}
