using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;

public sealed class FullName : ValueObject
{
    public FullName(string surname, string name, string patronymic)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
    }

    public string Surname { get; }
    public string Name { get; }
    public string Patronymic { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Surname;
        yield return Name;
        yield return Patronymic;
    }
}
