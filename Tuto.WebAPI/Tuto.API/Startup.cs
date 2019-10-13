using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tuto.API.Authorization;
using Tuto.API.Configuration;
using Tuto.Domain;
using Tuto.Domain.Authorization;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;
using Tuto.Services;
using Tuto.Services.Interfaces;

namespace Tuto.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<OAuthConfig>(Configuration.GetSection("GoogleOAuthService"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpClient();
            services.AddTransient<IRepository<Role>, GenericRepository<Role>>();
            services.AddTransient<IRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGoogleOAuthService, GoogleOAuthService>();
            services.AddSingleton<ISessionStorage<AppUser>, SessionMemoryStorage<AppUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<AuthMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
