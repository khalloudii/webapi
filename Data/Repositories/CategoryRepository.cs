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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly StoreContext _context;
        public CategoryRepository(StoreContext context): base(context)
        {
            _context = context;
        }

    }
}
