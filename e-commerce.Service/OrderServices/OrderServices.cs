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

namespace e_commerce.Service.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public OrderServices(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseModel>> GetAll()
        {
            return await _context.Orders.Select(order => _mapper.Map<OrderResponseModel>(order)).ToListAsync();
        }

        public async Task<OrderResponseModel> GetById(int? id)
        {
            return _mapper.Map<OrderResponseModel>(await _context.Orders.FindAsync(id));
        }

        public async Task<OrderResponseModel> Create(OrderRequestModel orderRequest)
        {
            var result = new OrderResponseModel();
            var orderEntities = _mapper.Map<OrderEntities>(orderRequest);

            _context.Orders.Add(orderEntities);
            await _context.SaveChangesAsync();

            if (orderEntities.Id > 0)
                result = await GetById(orderEntities.Id);

            return result;
        }

        public async Task<bool> Delete(int? id)
        {
            var isRemoved = false;

            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                var orderIdRemoved = await _context.SaveChangesAsync();
                isRemoved = orderIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }

        public async Task<OrderResponseModel> Update(OrderRequestModel orderRequest, int? id)
        {
            var order = await _context.Orders.FindAsync(id);

            _mapper.Map(orderRequest, order);
            var orderIdUpdated = await _context.SaveChangesAsync();

            if (orderIdUpdated > 0)
                return _mapper.Map<OrderResponseModel>(order);

            return new OrderResponseModel();
        }
    }
}
