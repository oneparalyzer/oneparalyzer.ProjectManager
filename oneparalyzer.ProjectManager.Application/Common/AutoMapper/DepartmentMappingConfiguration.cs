using AutoMapper;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public sealed class DepartmentMappingConfiguration : Profile
{
    public DepartmentMappingConfiguration()
    {
        CreateMap<Department, GetDepartmentByIdModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.OfficeId, opt => opt.MapFrom(src => src.OfficeId.Value));
        
        CreateMap<Department, GetDepartmentsByPageModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.OfficeId, opt => opt.MapFrom(src => src.OfficeId.Value));
    }
    
}