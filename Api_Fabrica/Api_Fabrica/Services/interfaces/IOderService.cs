using Api_Fabrica.Dto;
using Api_Fabrica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Services.interfaces
{
    public interface IOrderService
    {
        public List<OrderEntity> GetOrders();

        public OrderEntity GetOrderByiD(int id);

        public OrderEntity AddOrder(OrderEntity orderItem);

        public OrderEntity UpdateOrder(int id, OrderEntity orderItem);

        public Boolean DeleteOrder(int id);



    }
}
