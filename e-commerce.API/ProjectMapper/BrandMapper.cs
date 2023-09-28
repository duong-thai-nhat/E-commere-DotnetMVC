using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<BrandEntities, BrandRequestModel>();
            CreateMap<BrandRequestModel, BrandEntities>();

            CreateMap<BrandEntities, BrandResponseModel>();
            CreateMap<BrandResponseModel, BrandEntities>();
        }
    }
}
