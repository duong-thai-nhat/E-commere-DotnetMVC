using e_commerce.Model.Models;
using e_commerce.Service.ParentCategoryServices;
using e_commerce.Service.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Service.UserServices;

namespace e_commerce.Controllers
{
    public class ParentCategoryController : BaseController
    {
        private readonly IParentCategoryServices _parentCategoryServices;

        public ParentCategoryController(IParentCategoryServices parentCategoryServices)
        {
            _parentCategoryServices = parentCategoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _parentCategoryServices.GetParentCategoryAll();

            if (result == null)
            {
                return NotFound("Không tồn tại Danh mục cha nào!");
            }

            return Ok(result);
        }

        [HttpGet("{parentCategoryId}")]
        public async Task<IActionResult> GetById(int? parentCategoryId)
        {
            if (parentCategoryId is null || parentCategoryId <= 0)
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            var result = await _parentCategoryServices.GetParentCategoryById(parentCategoryId);

            if (result == null)
                return NotFound("Không tồn tại danh mục cha nào!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(ParentCategoryRequestModel parentCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập đầy đủ thông tin vào.");

            var result = await _parentCategoryServices.CreateParentCategory(parentCategory);
            if (result == null)
                return NotFound("Tạo không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? parentCategoryId)
        {
            if (parentCategoryId is null || parentCategoryId <= 0)
            {
                return BadRequest("Vui lòng truyền parentCategoryId và lớn hơn 0");
            }
            var parentCategoryExists = await _parentCategoryServices.GetParentCategoryById(parentCategoryId);
            if (parentCategoryExists == null)
                return NotFound("Danh mục cha không tồn tại!");

            var isRemoved = await _parentCategoryServices.DeleteParentCategory(parentCategoryId);
            if (!isRemoved)
                return NotFound("Xóa danh mục cha thất bại");

            return Ok("Xóa danh mục cha thành công!");
        }

        [HttpPut("{parentCategoryId}")]
        public async Task<IActionResult> Update(ParentCategoryRequestModel parentCategoryRequest, int? parentCategoryId)
        {
            if (ModelState.IsValid && parentCategoryId > 0)
            {
                var result = await _parentCategoryServices.UpdateParentCategory(parentCategoryRequest, parentCategoryId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui lòng truyền thông tin.");
            }
        }
    }
}
