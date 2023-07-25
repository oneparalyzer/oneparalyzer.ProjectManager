namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;

public class GetEmployeeByIdModel
{
    public Guid Id { get; set; }
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}