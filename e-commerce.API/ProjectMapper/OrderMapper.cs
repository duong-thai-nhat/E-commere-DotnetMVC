using AutoMapper;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;

namespace e_commerce.ProjectMapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<OrderEntities, OrderRequestModel>();
            CreateMap<OrderRequestModel, OrderEntities>();

            CreateMap<OrderEntities, OrderResponseModel>();
            CreateMap<OrderResponseModel, OrderEntities>();
        }
    }
}
