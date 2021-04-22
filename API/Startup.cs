using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TheRoom.PromoCodes.API.Interfaces;
using TheRoom.PromoCodes.API.Models.Requests;
using TheRoom.PromoCodes.API.Models.Responses;
using TheRoom.PromoCodes.API.Services;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;
using TheRoom.PromoCodes.Infrastructure.Data;
using TheRoom.PromoCodes.Infrastructure.Identity;
using TheRoom.PromoCodes.Infrastructure.Services;

namespace API
{
    public class Startup
    {
        private const string _CORS_POLICY = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            // use real database
            //ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            services.AddDbContext<PromoCodesDbContext>(c =>
                c.UseInMemoryDatabase("PromoCodes"));

            services.AddDbContext<PromoCodesIdentityDbContext>(options =>
                options.UseInMemoryDatabase("PromoCodesIdentity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<PromoCodesDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("PromoCodesDbConnection")));

            // Add Identity DbContext
            services.AddDbContext<PromoCodesIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PromoCodesIdentityDbConnection")));

            ConfigureServices(services);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<PromoCodesIdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggingService<>));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IMapper<Service, ServiceRequest, ServiceResponse>, ServiceMapper>();
            services.AddScoped<IMapper<UserBonus, BonusActivationRequest, UserBonusResponse>, UserBonusMapper>();
            services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();


            services.AddMemoryCache();

            byte[] key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _CORS_POLICY,
                                  builder =>
                                  {
                                      builder.WithOrigins();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });

            services.AddControllers();
            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_CORS_POLICY);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
