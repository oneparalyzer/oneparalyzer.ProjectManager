using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Infrastructure.Persistance;

namespace oneparalyzer.ProjectManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrasructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("MSSQL");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => 
            options.UseSqlServer(connectionString));
        return services;
    }
}
