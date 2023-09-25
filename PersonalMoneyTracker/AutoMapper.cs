﻿using AutoMapper;
using PersonalMoneyTracker.Core.Models;
using PersonalMoneyTracker.Dtos;

namespace PersonalMoneyTracker
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserRegisterDto, User>();

        }
    }
}
