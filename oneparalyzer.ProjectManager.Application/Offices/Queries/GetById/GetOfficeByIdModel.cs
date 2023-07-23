namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;

public class GetOfficeByIdModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid CompanyId { get; set; }
}