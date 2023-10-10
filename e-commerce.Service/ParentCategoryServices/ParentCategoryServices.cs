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
            return await eCommerce.ParentCategories.Select(category => _mapper.Map<ParentCategoryResponseModel>(category)).ToListAsync();
        }

        public async Task<ParentCategoryResponseModel> GetParentCategoryById(int? parentCategoryId)
        {
            return _mapper.Map<ParentCategoryResponseModel>(await eCommerce.ParentCategories.FindAsync(parentCategoryId));
        }

        public async Task<ParentCategoryResponseModel> CreateParentCategory(ParentCategoryRequestModel parentCategoryRequest)
        {
            var result = new ParentCategoryResponseModel();
            var parentCategoryEntities = _mapper.Map<ParentCategoryEntities>(parentCategoryRequest);

            eCommerce.ParentCategories.Add(parentCategoryEntities);
            await eCommerce.SaveChangesAsync();

            if (parentCategoryEntities.ParentCategoryId > 0)
                result = await GetParentCategoryById(parentCategoryEntities.ParentCategoryId);
            return result;
        }

        public async Task<bool> DeleteParentCategory(int? parentCategoryId)
        {
            bool isRemoved = false;

            var parentCategory = await eCommerce.ParentCategories.FindAsync(parentCategoryId);
            if (parentCategory != null)
            {
                eCommerce.ParentCategories.Remove(parentCategory);
                var parentCategoryIdRemoved = await eCommerce.SaveChangesAsync();
                isRemoved = parentCategoryIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }

        public async Task<ParentCategoryResponseModel> UpdateParentCategory(ParentCategoryRequestModel parentCategoryRequest, int? parentCategoryId)
        {
            var parentCategory = await eCommerce.ParentCategories.FindAsync(parentCategoryId);

            _mapper.Map(parentCategoryRequest, parentCategory);
            var parentCategoryIdUpdated = await eCommerce.SaveChangesAsync();

            if (parentCategoryIdUpdated > 0)
                return _mapper.Map<ParentCategoryResponseModel>(parentCategory);

            return new ParentCategoryResponseModel();
        }
    }
}
