using AutoMapper;
using EcommShop.Contracts.Dtos.User;
using EcommShop.DataAccessor.Entities;

namespace ECommShop.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserInfoDto, User>();

            CreateMap<User, UserInfoDto>();
        }
    }
}
