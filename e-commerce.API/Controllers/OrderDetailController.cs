using e_commerce.Model.Models;
using e_commerce.Service.CartServices;
using e_commerce.Service.OrderDetailServices;
using e_commerce.Service.OrderServices;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IOrderServices _orderServices;

        public OrderDetailController(IOrderDetailServices orderDetailServices, IOrderServices orderServices)
        {
            _orderDetailServices = orderDetailServices;
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int? orderId)
        {
            if (orderId is null || orderId <= 0)
            {
                return BadRequest("Vui lòng truyền OrderId và lớn hơn 0");
            }
            var order = await _orderServices.GetById(orderId);
            if (order == null)
                return NotFound("Không tồn tại đơn hàng nào!");
            var result = await _orderDetailServices.Get(orderId);
            if (result == null)
                return NotFound("Đơn đặt hàng này không tồn tại sản phẩm!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(OrderDetailRequestModel orderDetailRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin.");

            var result = await _orderDetailServices.CreateOrUpdate(orderDetailRequest);
            if (result == null)
                return NotFound("Tạo or sửa không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? orderId, int? productId)
        {
            if (orderId is null || orderId <= 0 || productId is null || productId <= 0)
            {
                return BadRequest("Vui lòng truyền orderId và productId hoặc truyền lớn hơn 0");
            }
            var orderExists = await _orderDetailServices.GetById(orderId, productId);
            if (orderExists == null)
                return NotFound("Không tồn tại sản phẩm nào để xóa!");

            var isRemoved = await _orderDetailServices.Delete(orderId, productId);
            if (!isRemoved)
                return NotFound("Xóa sản phẩm trong đơn đặt hàng thất bại");

            return Ok("Xóa sản phẩm trong đơn đặt hàng thành công!");
        }
    }
}
