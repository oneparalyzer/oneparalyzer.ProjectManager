using oneparalyzer.ProjectManager.Auth.Api.Entities;

namespace oneparalyzer.ProjectManager.Auth.Api.Helpers.Interfaces;

public interface IJwtTokenHelper
{
    string GenerateToken(User user, IList<string> roles);
}
