using Microsoft.AspNetCore.Mvc;

public class RetailController : Controller
{
    private readonly ApiService _apiService;

    public RetailController(ApiService apiService)
    {
        _apiService = apiService;
    }
        
    public async Task<IActionResult> Stores(string brand)
    {
        string data = await _apiService.GetRetailerDataAsync(brand, "Stores");
        return Content(data, "application/json");
    }

    public async Task<IActionResult> Categories(string brand, string storeId)
    {
        var data = await _apiService.GetRetailerDataAsync(brand, "Categories", new Dictionary<string, string>
        {
            { "storeId", storeId }
        });

        return Content(data, "application/json");
    }

    public async Task<IActionResult> Products(string brand, string categoryId)
    {
        var data = await _apiService.GetRetailerDataAsync(brand, "Products", new Dictionary<string, string>
        {
            { "categoryId", categoryId }
        });

        return Content(data, "application/json");
    }
}
