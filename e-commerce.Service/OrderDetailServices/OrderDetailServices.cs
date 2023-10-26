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

namespace e_commerce.Service.OrderDetailServices
{
    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public OrderDetailServices(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailResponseModel>> Get(int? orderId)
        {
            var orderDetails = await (from od in _context.OrderDetails
                               join p in _context.Products
                               on od.ProductId equals p.ProductID
                               where od.OrderId == orderId
                               select new OrderDetailResponseModel
                               {
                                   OrderId = od.OrderId,
                                   ProductId = od.ProductId,
                                   Product = p.ProductName,
                                   Quantity = od.Quantity,
                                   TotalPrice = od.TotalPrice,
                                   Note = od.Note
                               }).ToListAsync();

            return orderDetails;
        }

        public async Task<OrderDetailResponseModel> GetById(int? orderId, int? productId)
        {
            var orderDetail = await (from od in _context.OrderDetails
                                    join o in _context.Orders
                                    on od.OrderId equals o.Id
                                    join p in _context.Products
                                    on od.ProductId equals p.ProductID
                                    where od.OrderId == orderId && od.ProductId == productId
                                    select new OrderDetailResponseModel
                                    {
                                        OrderId = od.OrderId,
                                        ProductId = od.ProductId,
                                        Product = p.ProductName,
                                        Quantity = od.Quantity,
                                        TotalPrice = od.TotalPrice,
                                        Note = od.Note
                                    }).SingleOrDefaultAsync();

            return orderDetail ?? new OrderDetailResponseModel();
        }

        public async Task<OrderDetailResponseModel> CreateOrUpdate(OrderDetailRequestModel orderDetailRequest)
        {
            //productId, userId, quatity
            var result = new OrderDetailResponseModel();
            var existsOrderDetail = _context.OrderDetails.SingleOrDefault(
                od => od.OrderId == orderDetailRequest.OrderId && od.ProductId == orderDetailRequest.ProductId);
            //Update
            if (existsOrderDetail is not null)
            {
                //_mapper.Map(orderDetailRequest, existsOrderDetail);
                existsOrderDetail.OrderId = orderDetailRequest.OrderId;
                existsOrderDetail.ProductId = orderDetailRequest.ProductId;
                existsOrderDetail.Quantity = orderDetailRequest.Quantity;
                existsOrderDetail.Note = orderDetailRequest.Note;

                var priceProduct = await (from od in _context.OrderDetails
                                    join p in _context.Products
                                    on od.ProductId equals p.ProductID
                                    select p.Price).FirstOrDefaultAsync();

                existsOrderDetail.TotalPrice = orderDetailRequest.Quantity * priceProduct;

                var orderDetailIdUpdated = await _context.SaveChangesAsync();

                if (orderDetailIdUpdated > 0)
                    return await GetById(orderDetailRequest.OrderId, orderDetailRequest.ProductId);
            }
            //Create
            else
            {
                var priceProduct = await (from od in _context.OrderDetails
                                          join p in _context.Products
                                          on od.ProductId equals p.ProductID
                                          select p.Price).FirstOrDefaultAsync();
                var orderDetailEntities = new OrderDetailEntities
                {
                    OrderId = orderDetailRequest.OrderId,
                    ProductId = orderDetailRequest.ProductId,
                    Quantity = orderDetailRequest.Quantity,
                    Note = orderDetailRequest.Note,
                    TotalPrice = orderDetailRequest.Quantity * priceProduct
                };

                _context.OrderDetails.Add(orderDetailEntities);
                await _context.SaveChangesAsync();

                if (orderDetailEntities.ProductId > 0)
                    return await GetById(orderDetailRequest.OrderId, orderDetailRequest.ProductId);
            }
            return result;
        }

        public async Task<bool> Delete(int? orderId, int? productId)
        {
            bool isRemoved = false;

            var existsOrderDetail = _context.OrderDetails.SingleOrDefault(
                orderDetail => orderDetail.OrderId == orderId && orderDetail.ProductId == productId);

            if (existsOrderDetail is not null)
            {
                _context.OrderDetails.Remove(existsOrderDetail);
                var orderDetailIdRemoved = await _context.SaveChangesAsync();
                isRemoved = orderDetailIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }
    }
}
