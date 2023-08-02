using AutoMapper;
using oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;

namespace oneparalyzer.ProjectManager.Application.Common.AutoMapper;

public sealed class ProjectMappingConfiguration : Profile
{
    public ProjectMappingConfiguration()
    {
        CreateMap<Project, GetProjectByIdModel>()
            .ForPath(dst => dst.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForPath(dst => dst.DateStart, opt => opt.MapFrom(src => src.DateStart))
            .ForPath(dst => dst.Title, opt => opt.MapFrom(src => src.Title))
            .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(dst => dst.ProjectTasksModel.Select(x => x.Number), opt => opt.MapFrom(src => src.ProjectTasks.Select(x => x.Number)))
            .ForPath(dst => dst.ProjectTasksModel.Select(x => x.Description), opt => opt.MapFrom(src => src.ProjectTasks.Select(x => x.Description)))
            .ForPath(dst => dst.ProjectTasksModel.Select(x => x.IsCompleted), opt => opt.MapFrom(src => src.ProjectTasks.Select(x => x.IsCompleted)))
            .ForPath(dst => dst.ProjectTasksModel.Select(x => x.ResponsibleEmployeeId), opt => opt.MapFrom(src => src.ProjectTasks.Select(x => x.ResponsibleEmployeeId.Value)))
            .ForPath(dst => dst.ProjectTasksModel.Select(x => x.Id), opt => opt.MapFrom(src => src.ProjectTasks.Select(x => x.Id.Value)));

        CreateMap<Project, GetCompletedProjectsByPageModel>()
                .ForPath(dst => dst.DateStart, opt => opt.MapFrom(src => src.DateStart))
                .ForPath(dst => dst.Title, opt => opt.MapFrom(src => src.Title));
    }

}
