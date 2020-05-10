using AutoMapper;
using MCJPortal.Domain.Models;
using MCJPortal.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.ViewModels.Profiles
{
    public class ProjectLineProfile : Profile
    {
        public ProjectLineProfile()
        {
            CreateMap<ProjectLine, ProjectLineTableViewModel>()
                .ForMember(dest =>
                    dest.ProjectName,
                    opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest =>
                    dest.StatusName,
                    opt => opt.MapFrom(src => src.Status.Value))
                .ForMember(dest =>
                    dest.FileDoc,
                    opt => opt.MapFrom(src => src.Document.FileDoc));
            CreateMap<ProjectLine, ProjectLineViewModel>()
                .ForMember(dest =>
                    dest.ProjectName,
                    opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest =>
                    dest.FileDoc,
                    opt => opt.MapFrom(src => src.Document.FileDoc));
            CreateMap<ProjectLineViewModel, ProjectLine>()
                .ForPath(dest =>
                    dest.Document.FileDoc,
                    opt => opt.MapFrom(src => src.FileDoc))
                .ForPath(dest =>
                    dest.Project.Name,
                    opt => opt.MapFrom(src => src.ProjectName));
        }
    }
}
