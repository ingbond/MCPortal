using AutoMapper;
using MCJPortal.Domain.Models;
using MCJPortal.Domain.Models.Authorization;
using MCJPortal.ViewModels.ViewModels;
using MCJPortal.ViewModels.ViewModels.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCJPortal.ViewModels.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<IdentityRole, RoleViewModel>();
            CreateMap<RolePermission, RolePermissionViewModel>();

            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest =>
                    dest.RoleId,
                    opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().RoleId))
                .ForMember(dest =>
                    dest.UserProjectIds,
                    opt => opt.MapFrom(src => src.UserProjects.Select(x => x.ProjectId).ToList()));

            CreateMap<UserViewModel, ApplicationUser>()
                .ForMember(dest =>
                    dest.UserRoles,
                    opt => opt.MapFrom(src => new List<ApplicationUserRole> {
                        new ApplicationUserRole
                        {
                            RoleId = src.RoleId,
                            UserId = src.Id
                        } 
                    }))
                .ForMember(dest =>
                    dest.UserProjects,
                    opt => opt.MapFrom(src => src.UserProjectIds.Select(projectId => new UserProject
                    {
                        ProjectId = projectId,
                        UserId = src.Id
                    }).ToList())
            ); ;
        }
    }
}
