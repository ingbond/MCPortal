using AutoMapper;
using MCJPortal.Domain.Models;
using MCJPortal.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.ViewModels.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>();
            
        }
    }
}
