namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;

public class GetCompletedProjectsByPageModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime DateStart { get; set; }
}