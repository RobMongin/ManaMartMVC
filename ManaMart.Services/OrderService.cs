using ManaMart.Data;
using ManaMart.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Services
{
    public class OrderService
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        private readonly Guid _userId;
        public OrderService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var entity =
                new Order()
                {
                    OwnerId = _userId,
                    CustomerName = model.CustomerName,
                    OrderDate = model.OrderDate,
                    PhoneNumber = model.PhoneNumber
                };
            _ctx.Orders.Add(entity);
            return _ctx.SaveChanges() == 1;
        }
        //Get All orders regardless of user for admin
        public IEnumerable<OrderListItem> AdminGetAllOrders()
        {
            var entity = _ctx.Orders.ToList();
            var orders = entity.Select(e => new OrderListItem
            {
                OrderId = e.OrderId,
                OrderDate = e.OrderDate

            }).ToList();
            return orders;
        }

        public IEnumerable<OrderListItem> GetOrders()
        {
            var query =
                _ctx
                .Orders
                .Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                    new OrderListItem
                    {
                        OrderId = e.OrderId,
                        OrderDate = e.OrderDate
                    }
                    );
            return query.ToArray();
        }

        //Admin Get OrderById
        public OrderDetail AdminGetOrderById(int orderId)
        {
            var entity = _ctx.Orders.Find(orderId);
            var order = new OrderDetail
            {
                CustomerName = entity.CustomerName,
                OrderId = entity.OrderId,
                OrderDate = entity.OrderDate,
                PhoneNumber = entity.PhoneNumber
            };
            return order;
        }

        public OrderDetail GetOrderById(int id)
        {
            var entity =
                _ctx
                .Orders
                .Single(e => e.OrderId == id && e.OwnerId == _userId);
            OrderDetail detail =
                new OrderDetail
                {
                    CustomerName = entity.CustomerName,
                    OrderId = entity.OrderId,
                    OrderDate = entity.OrderDate,
                    PhoneNumber = entity.PhoneNumber
                };
            return detail;
        }

        public OrderEdit EditOrderById(int id)
        {
            var entity =
                _ctx
                .Orders
                .Single(e => e.OrderId == id && e.OwnerId == _userId);
            return new OrderEdit
            {
                CustomerName = entity.CustomerName,
                OrderId = entity.OrderId,
                OrderDate = entity.OrderDate,
                PhoneNumber = entity.PhoneNumber
            };
        }

        public bool UpdateOrder(OrderEdit model)
        {
            var entity =
                _ctx
                .Orders
                .Single(e => e.OrderId == model.OrderId && e.OwnerId == _userId);

            entity.OrderId = model.OrderId;
            entity.CustomerName = model.CustomerName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.OrderDate = model.OrderDate;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteOrder(int orderId)
        {
            var entity =
                _ctx
                .Orders
                .Single(e => e.OrderId == orderId && e.OwnerId == _userId);
            _ctx.Orders.Remove(entity);
            return _ctx.SaveChanges() == 1;
        }
    }
}
