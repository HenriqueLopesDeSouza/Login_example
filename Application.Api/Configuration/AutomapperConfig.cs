using Application.Api.ViewModels;
using Application.Business.Models;
using AutoMapper;

namespace Application.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
          
        }
    }
}
