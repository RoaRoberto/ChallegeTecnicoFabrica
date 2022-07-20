using Api_Fabrica.Dto;
using Api_Fabrica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Services.interfaces
{
    public interface IProductService
    {
        public List<ProductEntity> GetProducts();

        public ProductEntity GetProductByiD(int id);

        public ProductEntity AddProduct(ProductEntity productItem);

        public ProductEntity UpdateProduct(int id, ProductEntity productItem);

        public Boolean DeleteProduct(int id);



    }
}
