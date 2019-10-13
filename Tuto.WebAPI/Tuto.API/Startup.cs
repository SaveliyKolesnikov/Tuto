using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
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
            services.AddOData();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient();

            services.AddTransient<IRepository<Role>, GenericRepository<Role>>();
            services.AddTransient<IRepository<User>, GenericRepository<User>>();
            services.AddTransient<IRepository<TeacherInfo>, GenericRepository<TeacherInfo>>();
            services.AddTransient<IRepository<Lesson>, GenericRepository<Lesson>>();
            services.AddTransient<IRepository<ChatMessage>, GenericRepository<ChatMessage>>();
            services.AddTransient<IRepository<Review>, GenericRepository<Review>>();
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
                routes.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                routes.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            builder.EntitySet<TeacherInfo>("TeacherInfos");
            builder.EntitySet<Lesson>("Lessons");
            builder.EntitySet<ChatMessage>("ChatMessages");
            builder.EntitySet<Review>("Reviews");
            return builder.GetEdmModel();
        }
    }
}
