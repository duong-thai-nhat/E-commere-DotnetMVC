using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
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

        public async Task<List<CartResponseModel>> Get(int? userId)
        {
            var carts = await (from c in _context.Carts
                               join u in _context.Users
                               on c.UserId equals u.Id
                               join p in _context.Products
                               on c.ProductId equals p.ProductID
                               where c.UserId == userId
                               select new CartResponseModel
                               {
                                   UserId = c.UserId,
                                   ProductId = c.ProductId,
                                   User = u.UserName,
                                   Product = p.ProductName,
                                   Quantity = c.Quantity
                               }).ToListAsync();

            return carts;
        }

        public async Task<CartResponseModel> GetById(int? userId, int? productId)
        {
            var cart = await (from c in _context.Carts
                                            join u in _context.Users
                                            on c.UserId equals u.Id
                                            join p in _context.Products
                                            on c.ProductId equals p.ProductID
                                            where c.ProductId == productId && c.UserId == userId
                                            select new CartResponseModel
                                            {
                                                UserId = c.UserId,
                                                ProductId = c.ProductId,
                                                User = u.UserName,
                                                Product = p.ProductName,
                                                Quantity = c.Quantity
                                            }).SingleOrDefaultAsync();

            return cart ?? new CartResponseModel();
        }

        public async Task<CartResponseModel> CreateOrUpdate(CartRequestModel cartRequest)
        {
            //productId, userId, quatity
            var result = new CartResponseModel();
            var existsCart = _context.Carts.SingleOrDefault(c => c.UserId == cartRequest.UserId && c.ProductId == cartRequest.ProductId);
            if(existsCart is not null)
            {
                _mapper.Map(cartRequest, existsCart);
                var cartIdUpdated = await _context.SaveChangesAsync();

                if (cartIdUpdated > 0)
                    return await GetById(cartRequest.UserId, cartRequest.ProductId);
            }    
            else
            {
                var cartEntities = _mapper.Map<CartEntities>(cartRequest);
                _context.Carts.Add(cartEntities);
                await _context.SaveChangesAsync();

                if (cartEntities.ProductId > 0)
                    return await GetById(cartRequest.UserId, cartRequest.ProductId);
            }
            return result;
        }

        public async Task<bool> Delete(int? userId, int? productId)
        {
            bool isRemoved = false;

            var existsCart = _context.Carts.SingleOrDefault(cart => cart.UserId == userId && cart.ProductId == productId);

            if (existsCart is not null)
            {
                _context.Carts.Remove(existsCart);
                var cartIdRemoved = await _context.SaveChangesAsync();
                isRemoved = cartIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }
    }
}
