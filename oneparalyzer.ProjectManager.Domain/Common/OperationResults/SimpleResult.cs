namespace oneparalyzer.ProjectManager.Domain.Common.OperationResults;

public class SimpleResult
{
    private readonly List<string> _errors = new();
    public bool Succeed { get; private set; } = true;
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();
    public ValidationResult ValidationResult { get; } = new();

    public void AddError(string error)
    {
        Succeed = false;
        var existingError = _errors.FirstOrDefault(x => x == error);
        if (string.IsNullOrWhiteSpace(existingError))
        {
            _errors.Add(error);
        }
    }
    
    public void AddErrors(List<string> errors)
    {
        foreach (string error in errors)
        {
            AddError(error);
        }
    }

    public void AddValidationErrors(IDictionary<string, string[]> validationErrors)
    {
        ValidationResult.Errors = validationErrors;
    }
}
