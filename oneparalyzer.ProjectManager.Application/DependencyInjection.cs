using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Create;
using oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Update;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Create;
using oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Update;
using oneparalyzer.ProjectManager.Application.Projects.Commands.Create;
using System.Reflection;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

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
        services.AddTransient<IValidator<GetCompanyByIdQuery>, GetCompanyByIdQueryValidator>();
        services.AddTransient<IValidator<GetCompaniesByPageQuery>, GetCompaniesByPageQueryValidator>();
        services.AddTransient<IValidator<UpdateCompanyCommand>, UpdateCompanyCommandValidator>();

        services.AddTransient<IValidator<CreateOfficeCommand>, CreateOfficeCommandValidator>();
        services.AddTransient<IValidator<RemoveOfficeByIdCommand>, RemoveOfficeByIdCommandValidator>();
        services.AddTransient<IValidator<UpdateOfficeCommand>, UpdateOfficeCommandValidator>();
        services.AddTransient<IValidator<GetOfficeByIdQuery>, GetOfficeByIdQueryValidator>();
        services.AddTransient<IValidator<GetOfficesByPageQuery>, GetOfficesByPageQueryValidator>();

        return services;
    }
}
