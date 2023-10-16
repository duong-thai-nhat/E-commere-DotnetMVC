using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.OrderServices
{
    public interface IOrderServices
    {
        Task<List<OrderResponseModel>> GetAll();
        Task<OrderResponseModel> GetById(int? id);
        Task<OrderResponseModel> Create(OrderRequestModel orderRequest);
        Task<bool> Delete(int? id);
        Task<OrderResponseModel> Update(OrderRequestModel orderRequest, int? id);
    }
}
