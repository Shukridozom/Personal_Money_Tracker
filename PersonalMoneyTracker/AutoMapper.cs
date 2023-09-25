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

            CreateMap<UserLoginDto, User>();

            CreateMap<User, UserLoginDto>()
                .ForMember(m => m.Id, opt => opt.Ignore());

        }
    }
}
