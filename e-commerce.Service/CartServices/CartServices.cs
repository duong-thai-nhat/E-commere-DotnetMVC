using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public CartServices(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CartResponseModel>> Get(int userId)
        {
            return await _context.Carts
                .Select(cart => _mapper.Map<CartResponseModel>(cart))
                .Where(cart => cart.UserId == userId)
                .ToListAsync();
        }
        
        public async Task<CartResponseModel> GetById(int userId, int productId)
        {
            return _mapper.Map<CartResponseModel>(await _context.Carts
                .Where(cart => cart.UserId == userId && cart.ProductId == productId)
                .ToListAsync());
        }
    }
}
