using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        private readonly StoreContext _context;
        public ProductTypeRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
