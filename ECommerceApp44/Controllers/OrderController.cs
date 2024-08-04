using ECommerceApp44.Service;
using Microsoft.AspNetCore.Mvc;
using Model;
using ECommerceApp44.dto;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ShoppingCartService _shoppingCartService;

    public OrderController(OrderService orderService, ShoppingCartService shoppingCartService)
    {
        _orderService = orderService;
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{orderId}")]
    public ActionResult<string> GetOrderStatus(int orderId)
    {
        var status = _orderService.GetOrderStatus(orderId);
        if (status == "Order Not Found")
        {
            return NotFound(status);
        }
        return Ok(status);
    }

    [HttpPost("cancel/{orderId}")]
    public ActionResult<string> CancelOrder(int orderId)
    {
        var result = _orderService.CancelOrder(orderId);
        if (result == "Cannot Cancel Order")
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder([FromBody] OrderRequest orderRequest)
    {
        Console.WriteLine(" --- Shopping Cart Service: " + _shoppingCartService);
        Console.WriteLine(" --- orderRequest: " + orderRequest);
        var cartItems = _shoppingCartService.GetCartItems(orderRequest.CartId);

        var order = _orderService.CreateOrder(orderRequest.CustomerId, cartItems, orderRequest.CartId, orderRequest.OrderStatus);

        return CreatedAtAction(nameof(GetOrderStatus), new { orderId = order.OrderId }, order);
    }
}


