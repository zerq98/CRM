using ApiApplication.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Product
{
    public interface IProductService
    {
        Task<IActionResult> AddProductAsync(ProductUpsertDto product,string userId);
        Task<IActionResult> DeleteProductAsync(int productId,string userId);
        Task<IActionResult> EditProductAsync(ProductUpsertDto product,string userId);
        Task<IActionResult> GetAllProductsAsync(int companyId,ProductFiltersDto dto,string userId);
    }
}
