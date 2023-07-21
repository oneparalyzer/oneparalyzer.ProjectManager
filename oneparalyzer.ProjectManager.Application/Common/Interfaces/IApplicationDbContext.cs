using Microsoft.EntityFrameworkCore;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;

namespace oneparalyzer.ProjectManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; set; }
    DbSet<Office> Offices { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<Post> Posts { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<ProjectTask> ProjectTasks { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
