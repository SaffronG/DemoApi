namespace FrontEnd.Components.Pages; 

using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Modals.HomeModals; 

public class HomeBase : ComponentBase {
    [Inject]
    public HttpClient Http { get; set; } = default!;

    protected AllLogs? messages { get; set; }
    
    protected override async Task OnInitializedAsync() 
    {
       await LoadExample(); 
    }

    protected async Task LoadExample() 
    {
        try 
        {
            messages = await Http.GetFromJsonAsync<AllLogs>("api/Messages/logs"); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching logs: {ex.Message}");
        }
    }
    protected async Task GetLogs() {
        
    }
    protected async Task GetUptime() {

    }
}
