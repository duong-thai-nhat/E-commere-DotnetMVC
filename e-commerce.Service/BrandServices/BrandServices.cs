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

namespace e_commerce.Service.BrandServices
{
    public class BrandServices : IBrandServices
    {
        private readonly ECommerceDbContext eCommerce;
        private readonly IMapper _mapper;
        public BrandServices(ECommerceDbContext eCommerce, IMapper mapper)
        {
            this.eCommerce = eCommerce;
            _mapper = mapper;
        }

        public async Task<List<BrandResponseModel>> GetBrandAll()
        {
            var brands = await eCommerce.Brands.Select(brand => _mapper.Map<BrandResponseModel>(brand)).ToListAsync();

            return brands;
        }

        public async Task<BrandResponseModel> GetBrandById(int? brandId)
        {
            var brand = await eCommerce.Brands.FindAsync(brandId);

            return _mapper.Map<BrandResponseModel>(brand);
        }

        public async Task<BrandResponseModel> CreateBrand(BrandRequestModel brandRequest)
        {
            var brand = _mapper.Map<BrandEntities>(brandRequest);

            eCommerce.Brands.Add(brand);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<BrandResponseModel>(brand);
        }

        public async Task<BrandResponseModel> DeleteBrand(int? brandId)
        {
            var brand = await eCommerce.Brands.FindAsync(brandId);

            if (brand == null)
            {
                return null;
            }

            eCommerce.Brands.Remove(brand);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<BrandResponseModel>(brand);
        }

        public async Task<BrandResponseModel> UpdateBrand(BrandRequestModel brandRequest, int? brandId)
        {
            var brand = await eCommerce.Brands.FindAsync(brandId);

            if (brand == null)
                return null;

            _mapper.Map(brandRequest, brand);

            await eCommerce.SaveChangesAsync();

            return _mapper.Map<BrandResponseModel>(brand);
        }
    }
}
