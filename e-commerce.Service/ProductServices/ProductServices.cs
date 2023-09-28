using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Service.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly ECommerceDbContext eCommerce;
        private readonly IMapper _mapper;
        public ProductServices(ECommerceDbContext eCommerce, IMapper mapper)
        {
            this.eCommerce = eCommerce;
            _mapper = mapper;
        }

        public async Task<List<ProductResponseModel>> GetProductAll()
        {
            var products = await eCommerce.Products.Select(product => _mapper.Map<ProductResponseModel>(product)).ToListAsync();

            return products;
        }

        public async Task<ProductResponseModel> GetProductById(int? productId)
        {
            var product = await eCommerce.Products.FindAsync(productId);

            return _mapper.Map<ProductResponseModel>(product);
        }

        public async Task<ProductResponseModel> CreateProduct(ProductRequestModel productRequest)
        {
            var productEntities = _mapper.Map<ProductEntities>(productRequest);

            eCommerce.Products.Add(productEntities);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ProductResponseModel>(productEntities);
        }

        public async Task<ProductResponseModel> DeleteProduct(int? productId)
        {
            var product = await eCommerce.Products.FindAsync(productId);

            if (product == null)
            {
                return null;
            }

            eCommerce.Products.Remove(product);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ProductResponseModel>(product);
        }

        public async Task<ProductResponseModel> UpdateProduct(ProductRequestModel productRequest, int? productId)
        {
            var product = await eCommerce.Products.FindAsync(productId);

            if (product == null)
                return null;

            _mapper.Map(productRequest, product);

            await eCommerce.SaveChangesAsync();

            return _mapper.Map<ProductResponseModel>(product);
        }
    }
}
