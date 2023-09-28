using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.CategoryServices
{
    public interface ICategoryServices
    {
        Task<List<CategoryResponseModel>> GetCategoryAll();
        Task<CategoryResponseModel> GetCategoryById(int? categoryId);
        Task<CategoryResponseModel> CreateCategory(CategoryRequestModel categoryRequest);
        Task<CategoryResponseModel> DeleteCategory(int? categoryId);
        Task<CategoryResponseModel> UpdateCategory(CategoryRequestModel categoryRequest, int? categoryId);
    }
}
