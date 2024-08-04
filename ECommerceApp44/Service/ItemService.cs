using System.Collections.Generic;
using System.Linq;
using ECommerceApp44.Config;
using Model;

public class ItemService
{
    private readonly CustomDbContext _context;

    public ItemService(CustomDbContext context)
    {
        _context = context;
    }

    public List<Item> GetAllItems()
    {
        return _context.Items.ToList();
    }

    public Item GetItemById(int itemId)
    {
        return _context.Items.Find(itemId);
    }

    public Item AddItem(Item item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
        return item;
    }

    public Item UpdateItem(int itemId, Item updatedItem)
    {
        var existingItem = _context.Items.Find(itemId);
        if (existingItem != null)
        {
            existingItem.ItemName = updatedItem.ItemName;
            existingItem.Price = updatedItem.Price;

            _context.SaveChanges();
        }
        return existingItem;
    }

    public bool DeleteItem(int itemId)
    {
        var item = _context.Items.Find(itemId);
        if (item != null)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}


