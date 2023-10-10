using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ECommerceDbContext eCommerce;
        private readonly IMapper _mapper;

        public CategoryServices(ECommerceDbContext eCommerce, IMapper mapper)
        {
            this.eCommerce = eCommerce;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseModel>> GetCategoryAll()
        {
            return await eCommerce.Categories.Select(category => _mapper.Map<CategoryResponseModel>(category)).ToListAsync();
        }

        public async Task<CategoryResponseModel> GetCategoryById(int? categoryId)
        {
            return _mapper.Map<CategoryResponseModel>(await eCommerce.Categories.FindAsync(categoryId));
        }

        public async Task<CategoryResponseModel> CreateCategory(CategoryRequestModel categoryRequest)
        {
            var result = new CategoryResponseModel();
            var categoryEntities = _mapper.Map<CategoryEntities>(categoryRequest);

            eCommerce.Categories.Add(categoryEntities);
            await eCommerce.SaveChangesAsync();

            if(categoryEntities.CategoryID > 0)
                result = await GetCategoryById(categoryEntities.CategoryID);

            return result;
        }

        public async Task<bool> DeleteCategory(int? categoryId)
        {
            var isRemoved = false;

            var category = await eCommerce.Categories.FindAsync(categoryId);

            if (category != null)
            {
                eCommerce.Categories.Remove(category);
                var categoryIdRemoved = await eCommerce.SaveChangesAsync();
                isRemoved = categoryIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }

        public async Task<CategoryResponseModel> UpdateCategory(CategoryRequestModel categoryRequest, int? categoryId)
        {
            var category = await eCommerce.Categories.FindAsync(categoryId);

            _mapper.Map(categoryRequest, category);
            var categoryIdUpdated = await eCommerce.SaveChangesAsync();

            if (categoryIdUpdated > 0)
                return _mapper.Map<CategoryResponseModel>(category);

            return new CategoryResponseModel();
        }
    }
}
