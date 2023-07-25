using AutoMapper;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public sealed class PostMappingConfiguration : Profile
{
    public PostMappingConfiguration()
    {
        CreateMap<Post, GetPostByIdModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId.Value));

        CreateMap<Post, GetPostsByPageModel>()
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId.Value));
    }
}
