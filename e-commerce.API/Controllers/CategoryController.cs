using e_commerce.Model.Models;
using e_commerce.Service.CategoryServices;
using e_commerce.Service.UserServices;
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
                return NotFound("Không tồn tại danh mục nào!");
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(int? categoryId)
        {
            if (categoryId is null || categoryId <= 0)
                return BadRequest("Vui lòng truyền CategoryId và lớn hơn 0!");
            var result = await _categoryServices.GetCategoryById(categoryId);

            if (result == null)
            {
                return NotFound("Không tồn tại danh mục với Id trên!");
            }
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(CategoryRequestModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin danh mục!");

            var result = await _categoryServices.CreateCategory(category);
            if (result == null)
                return NotFound("Tạo không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId is null || categoryId <= 0)
                return BadRequest("Vui lòng truyền categoryId và lớn hơn 0!");

            var categoryExists = await _categoryServices.GetCategoryById(categoryId);

            if(categoryExists == null)
                return NotFound("Danh mục không tồn tại!");

            var isRemoved = await _categoryServices.DeleteCategory(categoryId);

            if (!isRemoved)
                return NotFound("Xóa danh mục thất bại!");
            return Ok("Xóa người dùng thành công!");
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update(CategoryRequestModel categoryRequest, int? categoryId)
        {
            if (!ModelState.IsValid && categoryId <= 0)
            {
                return BadRequest("Vui lòng nhập thông tin!");
            }

            var categoryExists = await _categoryServices.GetCategoryById(categoryId);
            if (categoryExists == null)
                return NotFound();

            var result = await _categoryServices.UpdateCategory(categoryRequest, categoryId);

            if (result == null)
                return NotFound("Cập nhật thất bại !");

            return Ok(result);
        }
    }
}
