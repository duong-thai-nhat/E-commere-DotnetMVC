using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductEntities, ProductRequestModel>();
            CreateMap<ProductRequestModel, ProductEntities>();

            CreateMap<ProductEntities, ProductResponseModel>();
            CreateMap<ProductResponseModel, ProductEntities>();
        }
    }
}
