using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    // WITHOUT SET ......

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> Complete();

        public IProductRepository ProductRepository { get; }
        //public IOrderRepository OrderRepository { get; }
        //public IBrandRepository BrandRepository { get; }
        //public IProductTypeRepository ProductTypeRepository { get; }
        //public ICategoryRepository CategoryRepository { get; }

    }
}
