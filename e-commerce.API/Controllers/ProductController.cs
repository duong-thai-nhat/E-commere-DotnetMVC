using e_commerce.Model.Models;
using e_commerce.Service.CategoryServices;
using e_commerce.Service.ProductServices;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        public ProductController(IProductServices productServices, ICategoryServices categoryServices)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productServices.GetProductAll();
            if (result == null)
                return NotFound("Không tồn tại sản phẩm nào!");

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int? productId)
        {
            if (productId is null || productId <= 0)
            {
                return BadRequest("Vui lòng truyền productId và lớn hơn 0!");
            }

            var result = await _productServices.GetProductById(productId);

            if (result is null)
                return NotFound("Không tồn tại sản phẩm nào!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(ProductRequestModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vui lòng nhập thông tin sản phẩm!");

            //validate category id
            var categoryById = await _categoryServices.GetCategoryById(product.CategoryID);
            if(categoryById is null )
                return BadRequest("Role người dùng không hợp lệ.");

            var result = await _productServices.CreateProduct(product);
            if (result == null)
                return BadRequest("Tạo không thành công!");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId is null || productId <= 0)
               return BadRequest("Vui lòng truyền productId và lớn hơn 0!");
            
            var productExist =  await _productServices.GetProductById(productId);
            if (productExist == null)
                return NotFound("Sản phầm không tồn tại!");

            var isRemoved = await _productServices.DeleteProduct(productId);

            if (!isRemoved)
                return NotFound("Xóa sản phẩm thất bại!");
            return Ok("Xóa sản phầm thành công!");
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(ProductRequestModel productRequest, int productId)
        {
            if (!ModelState.IsValid || productId <= 0)
                return BadRequest("Vui lòng truyền productId và lớn hơn 0!");

            var productExist = await _productServices.GetProductById(productId);
            if (productExist == null)
                return NotFound("Sản phầm không tồn tại!");

            var result = await _productServices.UpdateProduct(productRequest, productId);

            if (result == null)
                return NotFound("Cập nhật sản phẩm thất bại!");

            return Ok("Cập nhật Role thành công!");
        }
    }
}
