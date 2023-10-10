using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<UserRoleEntities, UserRoleRequestModel>();
            CreateMap<UserRoleRequestModel, UserRoleEntities>();

            CreateMap<UserRoleEntities, UserRoleResponseModel>();
            CreateMap<UserRoleResponseModel, UserRoleEntities>();
        }
    }
}
