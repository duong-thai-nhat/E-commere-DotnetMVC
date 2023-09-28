using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserEntities, UserRequestModel>();
            CreateMap<UserRequestModel, UserEntities>();

            CreateMap<UserEntities, UserResponseModel>()
                .ForMember(dest => dest.FullName, action => action
                .MapFrom(src => src.FirstName + " " +  src.LastName))
                .ReverseMap();

            CreateMap<UserResponseModel, UserEntities>();
        }
    }
}
