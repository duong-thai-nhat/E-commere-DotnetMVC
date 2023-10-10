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
            var result = new ProductResponseModel();
            var productEntities = _mapper.Map<ProductEntities>(productRequest);

            eCommerce.Products.Add(productEntities);
            await eCommerce.SaveChangesAsync();

            if(productEntities.ProductID > 0)
                result = await GetProductById(productEntities.ProductID);

            return result;
        }

        public async Task<bool> DeleteProduct(int? productId)
        {
            bool isRemoved = false;

            var product = await eCommerce.Products.FindAsync(productId);

            if (product != null)
            {
                eCommerce.Products.Remove(product);
                var productIdRemoved = await eCommerce.SaveChangesAsync();
                isRemoved = productIdRemoved > 0;
                return isRemoved;
            }
            return isRemoved;
        }

        public async Task<ProductResponseModel> UpdateProduct(ProductRequestModel productRequest, int? productId)
        {
            var product = await eCommerce.Products.FindAsync(productId);

            _mapper.Map(productRequest, product);
            var productIdUpdate = await eCommerce.SaveChangesAsync();

            if (productIdUpdate > 0)
                return _mapper.Map<ProductResponseModel>(product);

            return new ProductResponseModel();
        }
    }
}
