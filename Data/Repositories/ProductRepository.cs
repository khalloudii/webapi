using Data.Entities;
using Data.Interfaces;
using Data.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context): base(context)
        {
            _context = context;
        }

        //count

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null,
            string sort = null)
        {

            IQueryable<Product> products = _context.Products;

            if (filter != null)
            {
                products = products.Where(filter);
            }

            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    default:
                        products = products.OrderBy(p => p.NameAR);
                        break;
                }
            }

            return await products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(string sort = null,
            int? brandId = null, int? typeId = null)
        {

            IQueryable<Product> products = _context.Products;

            if (brandId.HasValue)
            {
                products = products.Where(p => p.ProductBrandId == brandId);
            }

            if (typeId.HasValue)
            {
                products = products.Where(p => p.ProductTypeId == typeId);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    default:
                        products = products.OrderBy(p => p.NameAR);
                        break;
                }
            }

            return await products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(string sort = null,
            int? brandId = null, int? typeId = null, int? pageNo = null)
        {
            // where then sort then pagination
            IQueryable<Product> products = _context.Products;

            if (brandId.HasValue)
            {
                products = products.Where(p => p.ProductBrandId == brandId);
            }

            if (typeId.HasValue)
            {
                products = products.Where(p => p.ProductTypeId == typeId);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    default:
                        products = products.OrderBy(p => p.NameAR);
                        break;
                }
            }

            // suppose pagesize = 20
            int pagesize = 20;
            if (pageNo.HasValue)
            {
                int skip = pagesize * (pageNo.Value - 1);

                products = products.Skip(skip).Take(pagesize);
            }
            else
            {
                products = products.Take(pagesize);
            }

            return await products.ToListAsync();
        }
        //public async Task<IReadOnlyList<Product>> GetProductsAsync()
        //{
        //    return await _context.Products.ToListAsync();
        //}

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(ProductParams productParams)
        {
            // where then sort then pagination
            IQueryable<Product> products = _context.Products;

            if(!string.IsNullOrEmpty(productParams.Search))
            {
                products = products.Where(p => p.NameAR.ToLower().Contains(productParams.Search));
            }

            if (productParams.BrandId.HasValue)
            {
                products = products.Where(p => p.ProductBrandId == productParams.BrandId);
            }

            if (productParams.TypeId.HasValue)
            {
                products = products.Where(p => p.ProductTypeId == productParams.TypeId);
            }

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    default:
                        products = products.OrderBy(p => p.NameAR);
                        break;
                }
            }

            int skip = productParams.PageSize * (productParams.PageIndex - 1);

            products = products.Skip(skip).Take(productParams.PageSize);

            return await products.ToListAsync();
        }
    }
}
