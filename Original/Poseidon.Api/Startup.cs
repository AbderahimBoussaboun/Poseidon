using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Products;
using Poseidon.ApplicationServices.ResourceMaps.Services;
using Poseidon.ApplicationServices.ResourceMaps.Services.Products;
using Poseidon.Entities.ResourceMaps;
using Poseidon.Repositories.ResourceMaps;
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Interfaces.Applications;
using Poseidon.Repositories.ResourceMaps.Interfaces.Nodes;
using Poseidon.Repositories.ResourceMaps.Interfaces.Products;
using Poseidon.Repositories.ResourceMaps.Repositories.Products;
using Poseidon.Repositories.ResourceMaps.Repositories;
using Poseidon.Repositories.ResourceMaps.Interfaces.Pools;
using Poseidon.Repositories.ResourceMaps.Repositories.Pools;
using Poseidon.Repositories.ResourceMaps.Interfaces.Rules;
using Poseidon.Repositories.ResourceMaps.Repositories.Rules;
using Poseidon.Repositories.ResourceMaps.Interfaces.Monitors;
using Poseidon.Repositories.ResourceMaps.Repositories.Monitors;
using Poseidon.Repositories.ResourceMaps.Repositories.Applications;
using Poseidon.Repositories.ResourceMaps.Repositories.Nodes;
using Poseidon.Repositories.ResourceMaps.Interfaces.Balancers;
using Poseidon.Repositories.ResourceMaps.Repositories.Balancers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using Poseidon.Repositories.ResourceMaps.Repositories.Servers;
using Poseidon.Repositories.ResourceMaps.Repositories.Virtuals;
using Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Services.Applications;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Balancers;
using Poseidon.ApplicationServices.ResourceMaps.Services.Balancers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Services.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Services.Virtuals;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Virtuals;
using System.Text.Json.Serialization;

namespace Poseidon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            //avoid conflict with cycles on the bbdd relations.
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

            //bbdd configuration
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], x => x.MigrationsAssembly("Poseidon.Migrations")));

            services.AddSingleton<IConfiguration>(Configuration);

            //cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            //repositories
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IApplicationsRepository, ApplicationsRepository>();
            services.AddTransient<IBalancersRepository, BalancersRepository>();
            services.AddTransient<IServersRepository, ServersRepository>();
            services.AddTransient<IVirtualsRepository, VirtualsRepository>();
            services.AddTransient<INodesRepository, NodesRepository>();
            services.AddTransient<IPoolsRepository, PoolsRepository>();
            services.AddTransient<IRulesRepository, RulesRepository>();
            services.AddTransient<IMonitorsRepository, MonitorsRepository>();

            //Services

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IApplicationsService, ApplicationsService>();
            services.AddScoped<IComponentsService, ComponentsService>();
            services.AddScoped<ISubApplicationsService, SubApplicationsService>();
            services.AddScoped<IBalancersService, BalancersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IServersService, ServersService>();
            services.AddScoped<IServerApplicationsService, ServerApplicationsService>();
            services.AddScoped<IEnvironmentsService, EnvironmentsService>();
            services.AddScoped<IInfrastructuresService, InfrastructuresService>();
            services.AddScoped<IComponentTypesService, ComponentTypesService>();
            services.AddScoped<IVirtualsService, VirtualsService>();
            services.AddScoped<INodesRepository, NodesRepository>();
            services.AddScoped<IPoolsRepository, PoolsRepository>();
            services.AddScoped<IRulesRepository, RulesRepository>();
            services.AddScoped<IMonitorsRepository, MonitorsRepository>();


            //Api swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Poseidon.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            //dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins); // <---- Moved here

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/poseidonapi/swagger/v1/swagger.json", "Poseidon.Api v1"));

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("swagger/index.html");
                    return;
                }
                await next();
            });

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                /*endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");*/
            });
        }
    }
}
