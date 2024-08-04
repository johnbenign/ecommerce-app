using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public ActionResult<List<Item>> GetAllItems()
    {
        var items = _itemService.GetAllItems();
        return Ok(items);
    }

    [HttpGet("{itemId}")]
    public ActionResult<Item> GetItemById(int itemId)
    {
        var item = _itemService.GetItemById(itemId);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Item> AddItem([FromBody] Item item)
    {
        var createdItem = _itemService.AddItem(item);
        return CreatedAtAction(nameof(GetItemById), new { itemId = createdItem.ItemId }, createdItem);
    }

    [HttpPut("{itemId}")]
    public ActionResult<Item> UpdateItem(int itemId, [FromBody] Item updatedItem)
    {
        var item = _itemService.UpdateItem(itemId, updatedItem);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpDelete("{itemId}")]
    public ActionResult DeleteItem(int itemId)
    {
        var success = _itemService.DeleteItem(itemId);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}


