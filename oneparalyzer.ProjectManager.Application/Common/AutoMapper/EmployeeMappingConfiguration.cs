using AutoMapper;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public sealed class EmployeeMappingConfiguration : Profile
{
    public EmployeeMappingConfiguration()
    {
        CreateMap<Employee, GetEmployeeByIdModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.FullName.Name))
            .ForPath(dst => dst.Surname, opt => opt.MapFrom(src => src.FullName.Surname))
            .ForPath(dst => dst.PostId, opt => opt.MapFrom(src => src.PostId.Value))
            .ForPath(dst => dst.Patronymic, opt => opt.MapFrom(src => src.FullName.Patronymic))
            .ForPath(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId.Value));
        
        CreateMap<Employee, GetEmployeesByPageModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.FullName.Name))
            .ForPath(dst => dst.Surname, opt => opt.MapFrom(src => src.FullName.Surname))
            .ForPath(dst => dst.PostId, opt => opt.MapFrom(src => src.PostId.Value))
            .ForPath(dst => dst.Patronymic, opt => opt.MapFrom(src => src.FullName.Patronymic))
            .ForPath(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId.Value));
    }
}