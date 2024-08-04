using System;
using ECommerceApp44.Config;
using Model;

namespace ECommerceApp44.Service
{
	public class OrderService
	{
        private readonly CustomDbContext _context;

        public OrderService(CustomDbContext context)
        {
            _context = context;
        }

        public string GetOrderStatus(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                return order.OrderStatus;
            }
            return "Order Not Found";
        }

        public string CancelOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null && order.OrderStatus == "Not Shipped")
            {
                order.OrderStatus = "Cancelled";
                _context.SaveChanges();
                return "Order Cancelled";
            }
            return "Cannot Cancel Order";
        }

        public Order CreateOrder(int customerId, List<CartItem> cartItems, int cartId, string orderStatus)
        {
            var orderTotal = cartItems.Sum(item => item.Quantity * _context.Items.Find(item.ItemId).Price);

            var order = new Order
            {
                CustomerId = customerId,
                CartId = cartId,
                OrderStatus = orderStatus,
                OrderDate = DateTime.UtcNow,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }
    }
}

