using ApiApplication.DTO;
using ApiApplication.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddProductAsync(ProductUpsertDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            dto.CompanyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            return await _productService.AddProductAsync(dto);
        }

        [HttpDelete("Remove")]
        [Authorize]
        public async Task<IActionResult> RemoveProductAsync(int productId)
        {
            return await _productService.DeleteProductAsync(productId);
        }

        [HttpPatch("Edit")]
        [Authorize]
        public async Task<IActionResult> EditProductAsync(ProductUpsertDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            dto.CompanyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            return await _productService.EditProductAsync(dto);
        }

        [HttpPost("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(ProductFiltersDto dto)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var companyId = Convert.ToInt32(claimsIdentity.Claims.ToList().FirstOrDefault(x => x.Type == "companyId").Value);

            return await _productService.GetAllProductsAsync(companyId,dto);
        }
    }
}
