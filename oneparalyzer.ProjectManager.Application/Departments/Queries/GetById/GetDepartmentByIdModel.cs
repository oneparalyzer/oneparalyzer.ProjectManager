namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;

public class GetDepartmentByIdModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid OfficeId { get; set; }
}