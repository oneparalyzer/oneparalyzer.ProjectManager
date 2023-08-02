using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Create;
using oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Update;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Create;
using oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Update;
using oneparalyzer.ProjectManager.Application.Departments.Commands.Create;
using oneparalyzer.ProjectManager.Application.Departments.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Departments.Commands.Update;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Posts.Commands.Create;
using oneparalyzer.ProjectManager.Application.Posts.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Posts.Commands.Update;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Employees.Commands.Create;
using oneparalyzer.ProjectManager.Application.Employees.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Employees.Commands.Update;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Projects.Commands.Create;
using oneparalyzer.ProjectManager.Application.Projects.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Projects.Commands.Update;
using oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.CompleteById;
using oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Update;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.RemoveById;

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
        services.AddCompanyValidation();
        services.AddOfficeValidation();
        services.AddDepartmentValidation();
        services.AddPostValidation();
        services.AddEmployeeValidation();
        services.AddProjectValidation();
        services.AddProjectTaskValidation();
        
        return services;
    }

    private static IServiceCollection AddCompanyValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateCompanyCommand>, CreateCompanyCommandValidator>();
        services.AddTransient<IValidator<RemoveCompanyByIdCommand>, RemoveCompanyByIdCommandValidator>();
        services.AddTransient<IValidator<GetCompanyByIdQuery>, GetCompanyByIdQueryValidator>();
        services.AddTransient<IValidator<GetCompaniesByPageQuery>, GetCompaniesByPageQueryValidator>();
        services.AddTransient<IValidator<UpdateCompanyCommand>, UpdateCompanyCommandValidator>();
        
        return services;
    }
    
    private static IServiceCollection AddOfficeValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateOfficeCommand>, CreateOfficeCommandValidator>();
        services.AddTransient<IValidator<RemoveOfficeByIdCommand>, RemoveOfficeByIdCommandValidator>();
        services.AddTransient<IValidator<UpdateOfficeCommand>, UpdateOfficeCommandValidator>();
        services.AddTransient<IValidator<GetOfficeByIdQuery>, GetOfficeByIdQueryValidator>();
        services.AddTransient<IValidator<GetOfficesByPageQuery>, GetOfficesByPageQueryValidator>();
        
        return services;
    }
    
    private static IServiceCollection AddDepartmentValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateDepartmentCommand>, CreateDepartmentCommandValidator>();
        services.AddTransient<IValidator<RemoveDepartmentByIdCommand>, RemoveDepartmentByIdCommandValidator>();
        services.AddTransient<IValidator<UpdateDepartmentCommand>, UpdateDepartmentCommandValidator>();
        services.AddTransient<IValidator<GetDepartmentByIdQuery>, GetDepartmentByIdQueryValidator>();
        services.AddTransient<IValidator<GetDepartmentsByPageQuery>, GetDepartmentsByPageQueryValidator>();
        
        return services;
    }
    
    private static IServiceCollection AddPostValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
        services.AddTransient<IValidator<RemovePostByIdCommand>, RemovePostByIdCommandValidator>();
        services.AddTransient<IValidator<UpdatePostCommand>, UpdatePostCommandValidator>();
        services.AddTransient<IValidator<GetPostByIdQuery>, GetPostByIdQueryValidator>();
        services.AddTransient<IValidator<GetProjectsByPageQuery>, GetPostsByPageQueryValidator>();
        
        return services;
    }
    
    private static IServiceCollection AddEmployeeValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
        services.AddTransient<IValidator<RemoveEmployeeByIdCommand>, RemoveEmployeeByIdCommandValidator>();
        services.AddTransient<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();
        services.AddTransient<IValidator<GetEmployeeByIdQuery>, GetEmployeeByIdQueryValidator>();
        services.AddTransient<IValidator<GetEmployeesByPageQuery>, GetEmployeesByPageQueryValidator>();
        
        return services;
    }
    
    private static IServiceCollection AddProjectValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateProjectCommand>, CreateProjectCommandValidator>();
        services.AddTransient<IValidator<RemoveProjectByIdCommand>, RemoveProjectByIdCommandValidator>();
        services.AddTransient<IValidator<UpdateProjectCommand>, UpdateProjectCommandValidator>();
        services.AddTransient<IValidator<GetProjectByIdQuery>, GetProjectByIdQueryValidator>();
        services.AddTransient<IValidator<GetCompletedProjectsByPageQuery>, GetCompletedProjectsByPageQueryValidator>();
        
        return services;
    }

    private static IServiceCollection AddProjectTaskValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CompleteProjectTaskByIdCommand>, CompleteProjectTaskByIdCommandValidator>();
        services.AddTransient < IValidator<AddProjectTaskCommand>, AddProjectTaskCommandValidator>();
        services.AddTransient<IValidator<UpdateProjectTaskCommand>, UpdateProjectTaskCommandValidator>();
        services.AddTransient<IValidator<RemoveProjectTaskByIdCommand>, RemoveProjectTaskByIdCommandValidator>();

        return services;
    }
}
