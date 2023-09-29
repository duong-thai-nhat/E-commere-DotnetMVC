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

namespace e_commerce.Service.ParentCategoryServices
{
    public class ParentCategoryServices : IParentCategoryServices
    {
        private readonly ECommerceDbContext eCommerce;
        private readonly IMapper _mapper;
        public ParentCategoryServices(ECommerceDbContext eCommerce, IMapper mapper)
        {
            this.eCommerce = eCommerce;
            _mapper = mapper;
        }

        public async Task<List<ParentCategoryResponseModel>> GetParentCategoryAll()
        {
            var parentCategories = await eCommerce.ParentCategories.Select(parentCategory => _mapper.Map<ParentCategoryResponseModel>(parentCategory)).ToListAsync();

            return parentCategories;
        }

        public async Task<ParentCategoryResponseModel> GetParentCategoryById(int? parentCategoryId)
        {
            var parentCategory = await eCommerce.ParentCategories.FindAsync(parentCategoryId);

            return _mapper.Map<ParentCategoryResponseModel>(parentCategory);
        }

        public async Task<ParentCategoryResponseModel> CreateParentCategory(ParentCategoryRequestModel parentCategoryRequest)
        {
            var parentCategory = _mapper.Map<ParentCategoryEntities>(parentCategoryRequest);

            eCommerce.ParentCategories.Add(parentCategory);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ParentCategoryResponseModel>(parentCategory);
        }

        public async Task<ParentCategoryResponseModel> DeleteParentCategory(int? parentCategoryId)
        {
            var parentCategory = await eCommerce.ParentCategories.FindAsync(parentCategoryId);

            if (parentCategory == null)
            {
                return null;
            }

            eCommerce.ParentCategories.Remove(parentCategory);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ParentCategoryResponseModel>(parentCategory);
        }

        public async Task<ParentCategoryResponseModel> UpdateParentCategory(ParentCategoryRequestModel parentCategoryRequest, int? parentCategoryId)
        {
            var parentCategory = await eCommerce.ParentCategories.FindAsync(parentCategoryId);

            if (parentCategory == null)
                return null;

            _mapper.Map(parentCategoryRequest, parentCategory);

            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ParentCategoryResponseModel>(parentCategory);
        }
    }
}
