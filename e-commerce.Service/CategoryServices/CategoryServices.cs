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
            var categories = await eCommerce.Categories.Select(category => _mapper.Map<CategoryResponseModel>(category)).ToListAsync();

            return categories;
        }

        public async Task<CategoryResponseModel> GetCategoryById(int? categoryId)
        {
            var category = await eCommerce.Categories.FindAsync(categoryId);

            return _mapper.Map<CategoryResponseModel>(category);
        }

        public async Task<CategoryResponseModel> CreateCategory(CategoryRequestModel categoryRequest)
        {
            var category = _mapper.Map<CategoryEntities>(categoryRequest);

            eCommerce.Categories.Add(category);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<CategoryResponseModel>(category);
        }

        public async Task<CategoryResponseModel> DeleteCategory(int? categoryId)
        {
            var category = await eCommerce.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            eCommerce.Categories.Remove(category);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<CategoryResponseModel>(category);
        }

        public async Task<CategoryResponseModel> UpdateCategory(CategoryRequestModel categoryRequest, int? categoryId)
        {
            var category = await eCommerce.Categories.FindAsync(categoryId);

            if (category == null)
                return null;

            _mapper.Map(categoryRequest, category);

            await eCommerce.SaveChangesAsync();

            return _mapper.Map<CategoryResponseModel>(category);
        }
    }
}
