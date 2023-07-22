using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Create;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Remove;
using System.Reflection;

namespace oneparalyzer.ProjectManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddLogging();
        services.AddValidation();
        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateCompanyCommand>, CreateCompanyCommandValidator>();
        services.AddTransient<IValidator<RemoveCompanyByIdCommand>, RemoveCompanyByIdCommandValidator>();
        return services;
    }
}
