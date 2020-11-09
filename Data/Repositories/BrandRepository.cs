using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class BrandRepository : GenericRepository<ProductBrand>, IBrandRepository
    {
        private readonly StoreContext _context;
        public BrandRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
