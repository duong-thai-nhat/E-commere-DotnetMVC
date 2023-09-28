using e_commerce.Model.Models;
using e_commerce.Service.BrandServices;
using e_commerce.Service.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandServices _brandServices;

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandServices.GetBrandAll();

            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brands);
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetById(int? brandId)
        {
            if (brandId != null || brandId > 0)
            {
                var result = await _brandServices.GetBrandById(brandId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(BrandRequestModel brand)
        {
            if (brand == null)
                return BadRequest("Invalid data");
            return Ok(await _brandServices.CreateBrand(brand));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? brandId)
        {
            if (brandId != null || brandId > 0)
            {
                var result = await _brandServices.DeleteBrand(brandId);
                if (result is null) return NotFound("Không tìm thấy thông tin dữ liệu");
                return Ok(result);
            }
            else
            {
                return BadRequest("Vui Lòng truyền thông tin");
            }
        }

        [HttpPut("{brandId}")]
        public async Task<IActionResult> Update(BrandRequestModel brandRequest, int? brandId)
        {
            if (ModelState.IsValid && brandId > 0)
            {
                var result = await _brandServices.UpdateBrand(brandRequest, brandId);
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
