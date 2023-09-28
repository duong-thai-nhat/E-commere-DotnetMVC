using e_commerce.Model.Models;

namespace e_commerce.Service.ProductServices
{
    public interface IProductServices
    {
        Task<ProductResponseModel> GetProductById(int? productId);
        Task<List<ProductResponseModel>> GetProductAll();
        Task<ProductResponseModel> CreateProduct(ProductRequestModel productRequest);
        Task<ProductResponseModel> DeleteProduct(int? productId);
        Task<ProductResponseModel> UpdateProduct(ProductRequestModel productRequest, int? productId);
    }
}
