using e_commerce.Model.Models;
using e_commerce.Service.ProductServices;
using e_commerce.Service.RoleServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class UserRoleController : BaseController
    {
        private readonly IRoleServices roleServices;
        public UserRoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await roleServices.GetAll();
            if (result == null)
                return NotFound("Không tồn tại Role nào!");

            return Ok(result);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetById(int? roleId)
        {
            if (roleId is null || roleId <= 0)
            {
                return BadRequest("Vui lòng truyền roleId và lớn hơn 0!");
            }

            var result = await roleServices.GetById(roleId);

            if (result is null)
                return NotFound("Không tồn tại Role nào!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRoleRequestModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin Role!");

            var result = await roleServices.Create(role);
            if (result == null)
                return BadRequest("Tạo không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? roleId)
        {
            if (roleId is null || roleId <= 0)
                return BadRequest("Vui lòng truyền roleId và lớn hơn 0!");

            var roleExist = await roleServices.GetById(roleId);
            if (roleExist == null)
                return NotFound("Role không tồn tại!");

            var isRemoved = await roleServices.Delete(roleId);

            if (!isRemoved)
                return NotFound("Xóa Role thất bại!");
            return Ok("Xóa Role thành công!");
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> Update(UserRoleRequestModel roleRequest, int roleId)
        {
            if (!ModelState.IsValid || roleId <= 0)
                return BadRequest("Vui lòng truyền roleId và lớn hơn 0!");

            var roleExist = await roleServices.GetById(roleId);
            if (roleExist == null)
                return NotFound("Role không tồn tại!");

            var result = await roleServices.Update(roleRequest, roleId);

            if (result == null)
                return NotFound("Cập nhật Role thất bại!");

            return Ok("Cập nhật Role thành công!");
        }
    }
}
