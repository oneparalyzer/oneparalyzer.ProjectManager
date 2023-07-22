namespace oneparalyzer.ProjectManager.Auth.Api.Contracts.OperationResults;

public class Result<TData> : SimpleResult
{
    public TData? Data { get; set; }
}
