using e_commerce.Model.Models;
using e_commerce.Service.CategoryServices;
using e_commerce.Service.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderServices.GetAll();

            if (orders == null)
            {
                return NotFound("Không tồn tại đơn đặt hàng nào!");
            }

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int? orderId)
        {
            if (orderId is null || orderId <= 0)
                return BadRequest("Vui lòng truyền orderId và lớn hơn 0!");
            var result = await _orderServices.GetById(orderId);

            if (result == null)
            {
                return NotFound("Không tồn tại đơn đặt hàng với Id trên!");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequestModel order)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin hóa đơn!");

            var result = await _orderServices.Create(order);
            if (result == null)
                return NotFound("Tạo đơn hàng không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? orderId)
        {
            if (orderId is null || orderId <= 0)
                return BadRequest("Vui lòng truyền orderId và lớn hơn 0!");

            var orderExists = await _orderServices.GetById(orderId);

            if (orderExists == null)
                return NotFound("Đơn hàng không tồn tại!");

            var isRemoved = await _orderServices.Delete(orderId);

            if (!isRemoved)
                return NotFound("Xóa đơn hàng thất bại!");
            return Ok("Xóa đươn hàng thành công!");
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> Update(OrderRequestModel orderRequest, int? orderId)
        {
            if (!ModelState.IsValid && orderId <= 0)
            {
                return BadRequest("Vui lòng nhập thông tin!");
            }

            var orderExists = await _orderServices.GetById(orderId);
            if (orderExists == null)
                return NotFound();

            var result = await _orderServices.Update(orderRequest, orderId);

            if (result == null)
                return NotFound("Cập nhật thất bại !");

            return Ok(result);
        }
    }
}
