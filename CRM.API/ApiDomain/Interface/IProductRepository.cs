using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task EditProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name, int companyId);
        Task<List<Product>> GetAllProductsAsync(int companyId,List<string> filters);
    }
}
