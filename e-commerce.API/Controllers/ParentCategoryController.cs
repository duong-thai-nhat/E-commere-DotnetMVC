using e_commerce.Model.Models;
using e_commerce.Service.ParentCategoryServices;
using e_commerce.Service.CategoryServices;
using Microsoft.AspNetCore.Mvc;

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
            var parentCategorys = await _parentCategoryServices.GetParentCategoryAll();

            if (parentCategorys == null)
            {
                return NotFound();
            }

            return Ok(parentCategorys);
        }

        [HttpGet("{parentCategoryId}")]
        public async Task<IActionResult> GetById(int? parentCategoryId)
        {
            if (parentCategoryId != null || parentCategoryId > 0)
            {
                var result = await _parentCategoryServices.GetParentCategoryById(parentCategoryId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(ParentCategoryRequestModel parentCategory)
        {
            if (parentCategory == null)
                return BadRequest("Invalid data");
            return Ok(await _parentCategoryServices.CreateParentCategory(parentCategory));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? parentCategoryId)
        {
            if (parentCategoryId != null || parentCategoryId > 0)
            {
                var result = await _parentCategoryServices.DeleteParentCategory(parentCategoryId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
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
