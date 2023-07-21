using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

public abstract class Id : ValueObject
{
    protected Id(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
