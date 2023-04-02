using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface IProductRepository
    {
        Task<Product> FindWhere(Expression<Func<Product, bool>> predicate, CancellationToken token);

        Task<Product> FindById(Guid id);

        IQueryable<Product> GetAll();

        public Task AddAsync(Product model);

        Task DeleteAsync(Product model);

        Task EditProductAsync(Product edit);
    }
}
