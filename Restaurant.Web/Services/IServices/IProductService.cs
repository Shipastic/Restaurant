using Restaurant.Web.Models;

namespace Restaurant.Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetSAllProductsAsync<T>();
        Task<T> GetProductByIDAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int id);

    }
}
