using Olwen_2._0._0.DataModel;
using Olwen_2._0._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olwen_2._0._0.Repositories.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<ProductModel>> GetAllProductModelsAsync();

        IEnumerable<ProductModel> GetAllProductModels();

        IEnumerable<ProductModel> GetProductModelsFilter(Func<Product, bool> predicate);
    }
}
