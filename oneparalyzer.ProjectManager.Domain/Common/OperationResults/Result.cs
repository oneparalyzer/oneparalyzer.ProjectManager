﻿namespace oneparalyzer.ProjectManager.Domain.Common.OperationResults;

public class Result<TData> : SimpleResult
{
    public TData? Data { get; set; }
}
