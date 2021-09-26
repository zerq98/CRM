using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiDomain.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> AddProductAsync(ProductUpsertDto product)
        {
            try
            {
                var productToDB = _mapper.Map<ApiDomain.Entity.Product>(product);
                return new JsonResult(new ApiResponse<ProductUpsertDto>()
                {
                    Code = 201,
                    ErrorMessage = "",
                    Data = _mapper.Map<ProductUpsertDto>(await _productRepository.AddProductAsync(productToDB))
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Code = 500,
                    Data = null,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }

        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            try
            {
                await _productRepository.DeleteProductAsync(productId);
                return new JsonResult(new ApiResponse<object>
                {
                    Code = 200,
                    Data = null,
                    ErrorMessage = ""
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Code = 500,
                    Data = null,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }

        public async Task<IActionResult> EditProductAsync(ProductUpsertDto product)
        {
            try
            {
                ApiDomain.Entity.Product DBProduct = await _productRepository.GetProductByIdAsync(product.Id);

                if (DBProduct != null)
                {
                    DBProduct.MarkupRate = product.MarkupRate;
                    DBProduct.Name = product.Name;
                    DBProduct.UnitValue = product.UnitValue;
                    DBProduct.VatRate = product.VatRate;
                    await _productRepository.EditProductAsync(DBProduct);
                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 200,
                        Data = null,
                        ErrorMessage = ""
                    });
                }
                else
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Code = 404,
                        Data = null,
                        ErrorMessage = "Produkt nie odnaleziony."
                    });
                }
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Code = 500,
                    Data = null,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }

        public async Task<IActionResult> GetAllProductsAsync(int companyId,ProductFiltersDto dto)
        {
            try
            {
                var filterList = new List<string>
                {
                    dto.Name,
                    dto.VatRate.Replace("%",""),
                    dto.MarkupRate.Replace("%","")
                };
                var products = _mapper.Map<List<ProductForListDto>>(await _productRepository.GetAllProductsAsync(companyId,filterList));

                return new JsonResult(new ApiResponse<List<ProductForListDto>>
                {
                    Code = 200,
                    Data = products,
                    ErrorMessage = ""
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Code = 500,
                    Data = null,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}
