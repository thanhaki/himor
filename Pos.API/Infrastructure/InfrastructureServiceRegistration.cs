using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pos.API.Application.Persistence;
using Pos.API.Common;
using Pos.API.Infrastructure.Persistence;
using Pos.API.Infrastructure.Repositories;
using Pos.API.Infrastructure.Services;
using Pos.API.Models;

namespace Pos.API.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBPosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IDonViRepository, DonViRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IVerifyCodeRepository, VerifyCodeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IMDataRepository, MDataRepository>();
            services.AddScoped<ISalerRepository, SalerRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Or you can also register as follows
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
