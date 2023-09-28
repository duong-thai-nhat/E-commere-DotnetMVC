using e_commerce.Model.Models;
using e_commerce.Service.CategoryServices;
using e_commerce.Service.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryServices.GetCategoryAll();

            if(categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(int? categoryId)
        {
            if (categoryId != null || categoryId > 0)
            {
                var result = await _categoryServices.GetCategoryById(categoryId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(CategoryRequestModel category)
        {
            if (category == null)
                return BadRequest("Invalid data");
            return Ok(await _categoryServices.CreateCategory(category));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId != null || categoryId > 0)
            {
                var result = await _categoryServices.DeleteCategory(categoryId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update(CategoryRequestModel categoryRequest, int? categoryId)
        {
            if (ModelState.IsValid && categoryId > 0)
            {
                var result = await _categoryServices.UpdateCategory(categoryRequest, categoryId);
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
