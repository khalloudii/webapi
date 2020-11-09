using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        //// Add All repositories handles here..
        private IProductRepository productRepository;
        //private IOrderRepository orderRepository;
        //private IBrandRepository brandRepository;
        //private IProductTypeRepository productTypeRepository;
        //private ICategoryRepository categoryRepository;


        //// Add All repositories getters here..
        public IProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(_context);
                }
                return productRepository;
            }
        }
        //public IOrderRepository OrderRepository
        //{
        //    get
        //    {
        //        if (this.orderRepository == null)
        //        {
        //            this.orderRepository = new OrderRepository(_context);
        //        }
        //        return orderRepository;
        //    }
        //}
        //public IBrandRepository BrandRepository
        //{
        //    get
        //    {
        //        if (this.brandRepository == null)
        //        {
        //            this.brandRepository = new BrandRepository(_context);
        //        }
        //        return brandRepository;
        //    }
        //}
        //public IProductTypeRepository ProductTypeRepository
        //{
        //    get
        //    {
        //        if (this.productTypeRepository == null)
        //        {
        //            this.productTypeRepository = new ProductTypeRepository(_context);
        //        }
        //        return productTypeRepository;
        //    }
        //}
        //public ICategoryRepository CategoryRepository
        //{
        //    get
        //    {
        //        if (this.categoryRepository == null)
        //        {
        //            this.categoryRepository = new CategoryRepository(_context);
        //        }
        //        return categoryRepository;
        //    }
        //}


        public IGenericRepository<T> Repository<T>() where T : class
        {
            //return new GenericRepository<T>(_context);
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator
                    .CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
