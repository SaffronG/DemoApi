using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Records.ItemRecords;

[Route("Test/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private static readonly Dictionary<Guid, Item> LocalItemList = new();

    [HttpGet]
    public IActionResult GetItems() {
        Guid nid = Guid.NewGuid();
        LocalItemList[nid] = new(nid, "Paints");
        return Ok(LocalItemList.Values);
    }

    [HttpPost]
    public IActionResult CreateItem([FromBody] Item newItem) {
        if (LocalItemList.TryAdd(newItem.Id, newItem)) {
            return Ok($"Successfully added {newItem.Name}");
        }
        return BadRequest("Item with given ID already exists!");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateItem(Guid id, [FromBody] string name) {
        if (!LocalItemList.ContainsKey(id)) {
            return NotFound($"Item {id} not found.");
        }
        
        LocalItemList[id] = new Item(id, name);
        return Ok($"Successfully updated item {id}");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteItem(Guid id) {
        if (LocalItemList.Remove(id)) {
            return Ok($"Successfully removed item with id of {id}");
        }
        return BadRequest($"Failed to remove item {id}, does not exist!");
    }
}
