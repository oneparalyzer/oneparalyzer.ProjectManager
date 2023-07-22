namespace oneparalyzer.ProjectManager.Auth.Api.Contracts.OperationResults;

public class ValidationResult
{
    private IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

    public bool IsValid { get; private set; } = true;
    public IDictionary<string, string[]> Errors
    {
        get => errors;
        set
        {
            if (errors is not null && errors.Count > 0)
            {
                IsValid = false;
                errors = value;
            }
        }
    }
}
