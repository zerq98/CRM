using ApiDomain.Entity;
using ApiDomain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                product.CreateDate = DateTime.Now;
                product.ModificationDate = product.CreateDate;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "ProductRepository/AddProductAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if (dbProduct != null)
                {
                    var positions = await _context.SellOpportunityPositions.Include(x=>x.OpportunityHeader).Where(x => x.ProductId == productId).ToListAsync();

                    foreach(var position in positions)
                    {
                        position.OpportunityHeader.SumGrossValue -= position.GrossValue;
                        position.OpportunityHeader.SumNetValue -= position.NetValue;
                        position.OpportunityHeader.SumVatValue -= position.VatValue;
                        position.OpportunityHeader.SumMarkupValue -= position.Markup;
                        _context.SellOpportunityPositions.Remove(position);
                    }
                    _context.Products.Remove(dbProduct);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "ProductRepository/AddProductAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public async Task EditProductAsync(Product product)
        {
            try
            {
                product.ModificationDate = DateTime.Now;
                _context.Products.Update(product);

                var positions = await _context.SellOpportunityPositions.Include(x => x.OpportunityHeader).Where(x => x.ProductId == product.Id).ToListAsync();

                foreach (var position in positions)
                {
                    position.OpportunityHeader.SumGrossValue -= position.GrossValue;
                    position.OpportunityHeader.SumNetValue -= position.NetValue;
                    position.OpportunityHeader.SumVatValue -= position.VatValue;
                    position.OpportunityHeader.SumMarkupValue -= position.Markup;

                    var qty = position.Quantity;
                    position.NetValue = product.UnitValue * qty;
                    position.VatValue = product.UnitValue * qty * product.VatRate;
                    position.GrossValue = position.NetValue + position.VatValue;
                    position.Markup = product.MarkupRate * product.UnitValue * qty;

                    position.OpportunityHeader.SumGrossValue += position.GrossValue;
                    position.OpportunityHeader.SumNetValue += position.NetValue;
                    position.OpportunityHeader.SumVatValue += position.VatValue;
                    position.OpportunityHeader.SumMarkupValue += position.Markup;
                    _context.SellOpportunityPositions.Update(position);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "ProductRepository/EditProductAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync(int companyId,List<string> filters)
        {
            var products = _context.Products
                .Where(x => x.CompanyId == companyId);

            if (filters[0] != "")
            {
                products = products.Where(x => x.Name.Contains(filters[0]));
            }
            if (filters[1] != "")
            {
                products = products.Where(x => x.VatRate==(Convert.ToDouble(filters[1])/100));
            }
            if (filters[2] != "")
            {
                products = products.Where(x => x.MarkupRate == (Convert.ToDouble(filters[1]) / 100));
            }

            return await products
                .OrderBy(x=>x.Name)
                .ThenBy(x=>x.VatRate)
                .ThenBy(x=>x.MarkupRate)
                .ThenBy(x=>x.UnitValue)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductByNameAsync(string name, int companyId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name==name && x.CompanyId==companyId);
        }
    }
}
