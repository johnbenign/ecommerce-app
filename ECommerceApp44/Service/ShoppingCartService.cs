using Microsoft.EntityFrameworkCore;
using ECommerceApp44.Config;
using Model;

public class ShoppingCartService
{
    private readonly CustomDbContext _context;

    public ShoppingCartService(CustomDbContext context)
    {
        _context = context;
    }

    public List<CartItem> GetCartItems(int cartId)
    {
        return _context.CartItems.Where(ci => ci.CartId == cartId).ToList();
    }

    public List<CartItem> AddItemToCart(int cartId, int itemId, int quantity)
    {
        var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ItemId == itemId);

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            _context.CartItems.Add(new CartItem { CartId = cartId, ItemId = itemId, Quantity = quantity });
        }

        _context.SaveChanges();
        return GetCartItems(cartId);
    }

    public List<CartItem> RemoveItemFromCart(int cartId, int itemId)
    {
        var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ItemId == itemId);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
        return GetCartItems(cartId);
    }
}
