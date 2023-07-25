namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;

public class GetPostByIdModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid DepartmentId { get; set; }
}
