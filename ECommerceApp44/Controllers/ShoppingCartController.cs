using Microsoft.AspNetCore.Mvc;
using Model;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{cartId}")]
    public ActionResult<List<CartItem>> GetCartItems(int cartId)
    {
        var items = _shoppingCartService.GetCartItems(cartId);
        return Ok(items);
    }

    [HttpPost("{cartId}/add")]
    public ActionResult<List<CartItem>> AddItemToCart(int cartId, int itemId, int quantity)
    {
        var items = _shoppingCartService.AddItemToCart(cartId, itemId, quantity);
        return Ok(items);
    }

    [HttpDelete("{cartId}/remove/{itemId}")]
    public ActionResult<List<CartItem>> RemoveItemFromCart(int cartId, int itemId)
    {
        var items = _shoppingCartService.RemoveItemFromCart(cartId, itemId);
        return Ok(items);
    }
}
