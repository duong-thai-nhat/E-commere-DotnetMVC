using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.CartServices
{
    public interface ICartServices
    {
        Task<List<CartResponseModel>> Get(int userId);
        Task<CartResponseModel> GetById(int userId,int productId);
        //Task<CartResponseModel> Create(CartRequestModel cartRequest);
        //Task<CategoryResponseModel> Delete(int userId, int productId);
        //Task<CategoryResponseModel> Update(CategoryRequestModel categoryRequest);
    }
}
