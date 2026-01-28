namespace front_end.Components.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

public partial class CrudModal : ComponentBase
{
    [Inject]
    protected HttpClient Client { get; set; } = default!;
    public List<Item> items = new();
    public bool loading = true;
    public string? error = null;
    public string modalName = "";
    public bool showModal = false;
    public bool isEditing = false;
    public Item? selectedItem = null;
    protected List<Item> ToList(Items wrapper)
    {
        return wrapper.items;
    }
    protected async Task SaveItem()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(modalName)) return;

            if (isEditing && selectedItem != null)
            {
                var updatedItem = selectedItem with { Name = modalName };
                await Client.PutAsJsonAsync($"Test/items/{selectedItem.Id}", updatedItem);
            }
            else
            {
                var newItem = new Item(Guid.NewGuid(), modalName);
                await Client.PostAsJsonAsync("Test/items", newItem);
            }

            await LoadItems();

            showModal = false;
            error = null;
        }
        catch (Exception e)
        {
            error = $"Save failed: {e.Message}";
        }
    }

    protected async Task LoadItems()
    {
        try
        {
            loading = true;
            error = null;
            items = await Client.GetFromJsonAsync<List<Item>>("Test/items") ?? new();
        }
        catch (Exception e)
        {
            error = e.Message;
        }
        finally
        {
            loading = false;
        }
    }

    protected async Task DeleteItem(Guid id)
    {
        try
        {
            error = null;
            await Client.DeleteAsync($"Test/items/{id}");
            await LoadItems();
        }
        catch (Exception e)
        {
            error = e.Message;
        }
    }

    protected async Task OpenCreateModal()
    {
        isEditing = false;
        modalName = "";
        selectedItem = null;
        showModal = true;
    }

    protected async Task OpenEditModal(Item item)
    {
        isEditing = true;
        selectedItem = item;
        modalName = item.Name;
        showModal = true;
    }

    protected async Task OpenDeleteModal(Item item)
    {
        selectedItem = item;
        await DeleteItem(item.Id);
    }

    protected async Task CloseModal()
    {
        showModal = false;
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadItems();
    }
}


public record Items(List<Item> items);
public record Item(
    [property: JsonPropertyName("id")] Guid Id, 
    [property: JsonPropertyName("name")] string Name
);