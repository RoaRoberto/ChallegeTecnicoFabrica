using Api_Fabrica.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Fabrica.Context;
using Api_Fabrica.Services.interfaces;
using Microsoft.Extensions.Configuration;

namespace Api_Fabrica.Services.implemaentation
{
    public class OrdersService : IOrderService

    {

        private readonly MyDbContext _myDbContext;
        public OrdersService(MyDbContext myDbContext, IConfiguration configuration)
        {
            this._myDbContext = myDbContext;
        }



        public OrderEntity AddOrder(OrderEntity orderItem)
        {
            var x = _myDbContext.Orders.Add(orderItem);
            _myDbContext.SaveChanges();
            return orderItem;
        }

        public bool DeleteOrder(int id)
        {
            var entity = _myDbContext.Orders.Find(id);
            if (entity != null)
            {
                var x = _myDbContext.Orders.Remove(entity);
                _myDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public OrderEntity GetOrderByiD(int id)
        {
            var entity = _myDbContext.Orders.Find(id);
            return entity;
        }

        public List<OrderEntity> GetOrders()
        {
            var all = _myDbContext.Orders.ToList();
            return all;
        }

        public OrderEntity UpdateOrder(int id, OrderEntity orderItem)
        {
            var resultado = _myDbContext.Orders.Find(id);

            if (resultado != null)
            {
                _myDbContext.Entry(resultado).CurrentValues.SetValues(orderItem);
                _myDbContext.SaveChanges();
                return orderItem;
            }
            return null;
        }
    }
}
