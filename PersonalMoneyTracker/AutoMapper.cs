using AutoMapper;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Dtos;

namespace PersonalMoneyTracker
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserRegisterDto>();

            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserLoginDto>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
