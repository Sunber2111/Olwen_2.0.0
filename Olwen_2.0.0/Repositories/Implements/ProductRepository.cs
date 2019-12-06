using Microsoft.EntityFrameworkCore;
using Olwen_2._0._0.DataModel;
using Olwen_2._0._0.DependencyInjection;
using Olwen_2._0._0.Model;
using Olwen_2._0._0.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olwen_2._0._0.Repositories.Implements
{
    public class ProductRepository : BaseAsyncRepository<Product>, IProductRepository
    {
        public async Task<IEnumerable<ProductModel>> GetAllProductModelsAsync()
        {
            var ds = await GetAllAsync();
            return ds.Select(
                            c => new ProductModel()
                            {
                                Name = c.Name,
                                ProductID = c.ProductID,
                                Picture = c.Picture.LoadImage(),
                                PriceOnOrder = c.UnitOnOrder,
                                UnitPrice = c.UnitPrice,
                                Quantity = SqlSupport.GetQuantityByProductId(c.ProductID)
                            });
        }

        public IEnumerable<ProductModel> GetProductModelsFilter(Func<Product, bool> predicate)
        {
            var ds = GetFilter(predicate);
            return ds.Select(
                            c => new ProductModel()
                            {
                                Name = c.Name,
                                ProductID = c.ProductID,
                                Picture = c.Picture.LoadImage(),
                                PriceOnOrder = c.UnitOnOrder,
                                UnitPrice = c.UnitPrice,
                                Quantity = SqlSupport.GetQuantityByProductId(c.ProductID)
                            });
        }

        public IEnumerable<ProductModel> GetAllProductModels()
        {
            return GetAll().Select(
                            c => new ProductModel()
                            {
                                Name = c.Name,
                                ProductID = c.ProductID,
                                Picture = c.Picture.LoadImage(),
                                PriceOnOrder = c.UnitOnOrder,
                                UnitPrice = c.UnitPrice,
                                Quantity = SqlSupport.GetQuantityByProductId(c.ProductID)
                            });
            
        }

        public async Task<IEnumerable<ProductStoreModel>> GetAllProductStoreByStoreIDAsync(int storeID)
        {
            var query = from c in dbcontext.StoreDetails
                        where c.Store.StoreID == storeID
                        select new ProductStoreModel()
                        {
                            Name = c.Product.Name,
                            ProductID = c.Product.ProductID,
                            Picture = c.Product.Picture.LoadImage(),
                            Quantity = c.Quantity
                        };
            return await query.ToListAsync();
        }

        public IEnumerable<ProductStoreModel> GetAllProductStoreByStoreID(int storeID)
        {
            var query = from c in dbcontext.StoreDetails
                        where c.Store.StoreID == storeID
                        select c;

            return query.ToList().Select(
                    c => new ProductStoreModel()
                    {
                        Name = c.Product.Name,
                        ProductID = c.Product.ProductID,
                        Picture = c.Product.Picture.LoadImage(),
                        Quantity = c.Quantity
                    }
                );
        }
    }
}
