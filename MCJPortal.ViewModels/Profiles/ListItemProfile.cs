using AutoMapper;
using MCJPortal.Domain.Models;
using MCJPortal.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCJPortal.ViewModels.Profiles
{
    public class ListItemProfile : Profile
    {
        public ListItemProfile()
        {
            CreateMap<ListItem, ListItemViewModel>();
            CreateMap<ListItemViewModel, EditPermissionViewModel>()
            .ForMember(dest =>
                    dest.Access,
                    opt => opt.MapFrom(src => src.Permissions.FirstOrDefault().Access));
        }
    }
}
