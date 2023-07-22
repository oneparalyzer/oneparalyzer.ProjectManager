using AutoMapper;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public sealed class CompanyMappingConfiguration : Profile
{
    public CompanyMappingConfiguration()
    {
        CreateMap<Company, GetCompanyByIdModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value));

        CreateMap<Company, GetCompaniesByPageModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value));
    }
}
