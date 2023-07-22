namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;

public class GetCompanyByIdModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
}
