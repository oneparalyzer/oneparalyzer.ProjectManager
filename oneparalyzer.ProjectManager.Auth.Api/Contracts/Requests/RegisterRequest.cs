namespace oneparalyzer.ProjectManager.Auth.Api.Contracts.Requests;

public class RegisterRequest : LoginRequest
{
    public string UserName { get; set; } = null!;
}
