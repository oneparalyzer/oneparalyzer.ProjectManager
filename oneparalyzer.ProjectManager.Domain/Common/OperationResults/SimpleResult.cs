namespace oneparalyzer.ProjectManager.Domain.Common.OperationResults;

public class SimpleResult
{
    private readonly List<string> _errors = new();
    public bool Succeed { get; private set; } = true;
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();
    public ValidationResult ValidationResult { get; } = new();

    public void AddError(string error)
    {
        if (Succeed)
        {
            Succeed = false;
        }

        var existingError = _errors.FirstOrDefault(x => x == error);
        if (string.IsNullOrWhiteSpace(existingError))
        {
            _errors.Add(error);
        }
    }
}
