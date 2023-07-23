using AutoMapper;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public class OfficeMappingConfiguration : Profile
{
    public OfficeMappingConfiguration()
    {
        CreateMap<Office, GetOfficeByIdModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.CompanyId, opt => opt.MapFrom(src => src.CompanyId.Value));
        
        CreateMap<Office, GetOfficesByPageModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.CompanyId, opt => opt.MapFrom(src => src.CompanyId.Value));
    }
}