using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryEntities, CategoryRequestModel>();
            CreateMap<CategoryRequestModel, CategoryEntities>();

            CreateMap<CategoryEntities, CategoryResponseModel>();
            CreateMap<CategoryResponseModel, CategoryEntities>();
        }
    }
}
