using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Infrastructure.Persistance;

namespace oneparalyzer.ProjectManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("MYSQL");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => 
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34))));
        return services;
    }
}
