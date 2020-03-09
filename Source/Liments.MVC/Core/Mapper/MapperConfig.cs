using AutoMapper;
using Liments.MVC.Core.Entities;
using Liments.MVC.Models;

namespace Liments.MVC.Core.Mapper
{
    public static class MapperConfig
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostViewModel, Post>();
            }).CreateMapper();
        }
    }
}
