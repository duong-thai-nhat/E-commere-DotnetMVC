using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.ParentCategoryServices
{
    public interface IParentCategoryServices
    {
        Task<List<ParentCategoryResponseModel>> GetParentCategoryAll();
        Task<ParentCategoryResponseModel> GetParentCategoryById(int? parentCategoryId);
        Task<ParentCategoryResponseModel> CreateParentCategory(ParentCategoryRequestModel parentCategoryRequest);
        Task<ParentCategoryResponseModel> DeleteParentCategory(int? parentCategoryId);
        Task<ParentCategoryResponseModel> UpdateParentCategory(ParentCategoryRequestModel parentCategoryRequest, int? parentCategoryId);
    }
}
