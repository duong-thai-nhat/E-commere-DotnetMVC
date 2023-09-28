using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.BrandServices
{
    public interface IBrandServices
    {
        Task<List<BrandResponseModel>> GetBrandAll();
        Task<BrandResponseModel> GetBrandById(int? brandId);
        Task<BrandResponseModel> CreateBrand(BrandRequestModel brandRequest);
        Task<BrandResponseModel> DeleteBrand(int? brandId);
        Task<BrandResponseModel> UpdateBrand(BrandRequestModel brandRequest, int? brandId);
    }
}
