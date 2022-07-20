using Api_Fabrica.Context;
using Api_Fabrica.Model;
using Api_Fabrica.Services.interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Services.implemaentation
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _myDbContext;

        public ProductService(MyDbContext myDbContext, IConfiguration configuration)
        {
            this._myDbContext = myDbContext;
        }
        public ProductEntity AddProduct(ProductEntity productItem)
        {
            var x = _myDbContext.Products.Add(productItem);
            _myDbContext.SaveChanges();
            return productItem;
        }

        public bool DeleteProduct(int id)
        {
            var entity = _myDbContext.Products.Find(id);
            if (entity != null)
            {
                var x = _myDbContext.Products.Remove(entity);
                _myDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public ProductEntity GetProductByiD(int id)
        {
            var entity = _myDbContext.Products.Find(id);
            return entity;
        }

        public List<ProductEntity> GetProducts()
        {
            var all = _myDbContext.Products.ToList();
            return all;
        }

        public ProductEntity UpdateProduct(int id, ProductEntity productItem)
        {
            var resultado = _myDbContext.Products.Find(id);

            if (resultado != null)
            {
                _myDbContext.Entry(resultado).CurrentValues.SetValues(productItem);
                _myDbContext.SaveChanges();
                return productItem;
            }
            return null;
        }
    }
}
