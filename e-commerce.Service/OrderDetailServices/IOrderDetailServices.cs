using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.OrderDetailServices
{
    public interface IOrderDetailServices
    {
        Task<List<OrderDetailResponseModel>> Get(int? orderId);
        Task<OrderDetailResponseModel> GetById(int? orderId, int? productId);
        Task<OrderDetailResponseModel> CreateOrUpdate(OrderDetailRequestModel orderDetailRequest);
        Task<bool> Delete(int? orderId, int? productId);
    }
}
