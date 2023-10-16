using e_commerce.Model.Models;
using e_commerce.Service.CartServices;
using e_commerce.Service.RoleServices;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartServices _cartServices;
        private readonly IUserServices _userServices;

        public CartController(ICartServices cartServices, IUserServices userServices)
        {
            _cartServices = cartServices;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartById(int? userId)
        {
            if (userId is null || userId <= 0)
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            var user = await _userServices.GetUserById(userId);
            if (user == null)
                return NotFound("Không tồn tại người dùng nào!");
            var result = await _cartServices.Get(userId);
            if (result == null)
                return NotFound("User này không tồn tại sản phẩm nào trong giỏ hàng!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CartRequestModel cartRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin.");

            var result = await _cartServices.CreateOrUpdate(cartRequest);
            if (result == null)
                return NotFound("Tạo or sửa không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? userId, int? productId)
        {
            if (userId is null || userId <= 0 || productId is null || productId <= 0)
            {
                return BadRequest("Vui lòng truyền UserId và productId hoặc truyền lớn hơn 0");
            }
            var userExists = await _cartServices.GetById(userId, productId);
            if (userExists == null)
                return NotFound("Không tồn tại sản phẩm nào để xóa!");

            var isRemoved = await _cartServices.Delete(userId, productId);
            if (!isRemoved)
                return NotFound("Xóa sản phẩm trong giỏ hàng thất bại");

            return Ok("Xóa sản phẩm trong giỏ hàng thành công!");
        }

    }
}
