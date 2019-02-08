using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using CarTransports.Data;
using CarTransports.Identity;
using CarTransports.Implementations;
using CarTransports.Infrastructure;
using CarTransports.Interfaces;

namespace CarTransports
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(c => { c.AddProfile(new ApplicationProfile()); });
            var mapper = config.CreateMapper();

            services.AddDbContext<AppContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppContext>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddSingleton(mapper);

            services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICurrentPositionRepository, CurrentPositionRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDebitStateRepository, DebitStateRepository>();
            services.AddTransient<IPickupPointRepository, PickupPointRepository>();
            services.AddTransient<IPickupRepository, PickupRepository>();
            services.AddTransient<IPickupStateRepository, PickupStateRepository>();
            services.AddTransient<IPortRepository, PortRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();

            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.Configure<WebEncoderOptions>(options => { options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); });

            Utils.RequireHttps(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            Utils.ErrorPages(app, env);
            Utils.AddSecurityHeaders(app);
            Utils.ClearCache(app);

            app.UseMvcWithDefaultRoute();
        }
    }
}
