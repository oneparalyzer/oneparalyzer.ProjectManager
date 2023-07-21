﻿namespace oneparalyzer.ProjectManager.Domain.SeedWorks;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
}
