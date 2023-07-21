using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;

public sealed class CompanyId : Id
{
    private CompanyId(Guid value) : base(value) { }

    public static CompanyId Create()
    {
        return new CompanyId(Guid.NewGuid());
    }

    public static CompanyId Create(Guid value)
    {
        return new CompanyId(value);
    }
}
