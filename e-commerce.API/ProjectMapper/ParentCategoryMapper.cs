using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class ParentCategoryMapper : Profile
    {
        public ParentCategoryMapper()
        {
            CreateMap<ParentCategoryEntities, ParentCategoryRequestModel>();
            CreateMap<ParentCategoryRequestModel, ParentCategoryEntities>();

            CreateMap<ParentCategoryEntities, ParentCategoryResponseModel>();
            CreateMap<ParentCategoryResponseModel, ParentCategoryEntities>();
        }
    }
}
