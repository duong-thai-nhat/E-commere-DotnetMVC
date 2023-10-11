using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<CartEntities, CartRequestModel>();
            CreateMap<CartRequestModel, CartEntities>();

            CreateMap<CartEntities, CartResponseModel>();
            CreateMap<CartResponseModel, CartEntities>();
        }
    }
}
