using oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;

public class GetProjectByIdModel : GetCompletedProjectsByPageModel
{
    public Guid EmployeeId { get; set; }
    public DateTime UpdatedDate { get; set; }
    public List<GetProjectTaskByIdModel> ProjectTasksModel { get; set; } = null!;
}
