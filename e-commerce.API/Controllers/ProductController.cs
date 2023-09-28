using e_commerce.Model.Models;
using e_commerce.Service.ProductServices;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await productServices.GetProductAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int? productId)
        {
            if (productId != null || productId > 0)
            {
                var result = await productServices.GetProductById(productId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(ProductRequestModel product)
        {
            if (product == null)
                return BadRequest("Invalid data");
            return Ok(await productServices.CreateProduct(product));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId != null || productId > 0)
            {
                var result = await productServices.DeleteProduct(productId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(ProductRequestModel productRequest, int productId)
        {
            if (ModelState.IsValid && productId > 0)
            {
                var result = await productServices.UpdateProduct(productRequest, productId);
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
