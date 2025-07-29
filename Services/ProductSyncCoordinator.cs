using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PriceHop.Models;
using System.Text.Json;

namespace PriceHop.Services;

public class ProductSyncCoordinator
{
    private readonly PriceHopDbContext _db;
    private readonly ApiService _api;
    private readonly ILogger<ProductSyncCoordinator> _logger;

    public ProductSyncCoordinator(PriceHopDbContext db, ApiService api, ILogger<ProductSyncCoordinator> logger)
    {
        _db = db;
        _api = api;
        _logger = logger;
    }

    private async Task SavePnsProductsAsync(List<JsonElement> products)
    {
        foreach (var product in products)
        {
            var entity = new PNS_Product
            {
                ProductId = product.GetProperty("productId").GetString() ?? Guid.NewGuid().ToString(),
                Name = product.GetProperty("name").GetString() ?? "",
                DisplayName = product.GetProperty("displayName").GetString() ?? "",
                SaleType = product.GetProperty("saleType").GetString() ?? "",
                RestrictedFlag = product.GetProperty("restrictedFlag").GetBoolean(),
                LiquorFlag = product.GetProperty("liquorFlag").GetBoolean(),
                TobaccoFlag = product.GetProperty("tobaccoFlag").GetBoolean(),
                OriginRegulated = product.GetProperty("originRegulated").GetBoolean(),
                CateredFlag = product.TryGetProperty("cateredFlag", out var catered) && catered.GetBoolean(),
                Brand = product.TryGetProperty("brand", out var brand) ? brand.GetString() : null,
                OriginStatement = product.TryGetProperty("originStatement", out var origin) ? origin.GetString() : null,
                LastUpdated = DateTime.UtcNow
            };

            var existing = await _db.PNS_Product.FindAsync(entity.ProductId);
            if (existing == null)
                _db.PNS_Product.Add(entity);
            else
                _db.Entry(existing).CurrentValues.SetValues(entity);
        }

        await _db.SaveChangesAsync();
    }

    private async Task SaveNewWorldProductsAsync(List<JsonElement> products)
    {
        foreach (var product in products)
        {
            var entity = new NW_Product
            {
                ProductId = product.GetProperty("productId").GetString() ?? Guid.NewGuid().ToString(),
                Name = product.GetProperty("name").GetString() ?? "",
                DisplayName = product.GetProperty("displayName").GetString() ?? "",
                SaleType = product.GetProperty("saleType").GetString() ?? "",
                RestrictedFlag = product.GetProperty("restrictedFlag").GetBoolean(),
                LiquorFlag = product.GetProperty("liquorFlag").GetBoolean(),
                TobaccoFlag = product.GetProperty("tobaccoFlag").GetBoolean(),
                OriginRegulated = product.GetProperty("originRegulated").GetBoolean(),
                CateredFlag = product.TryGetProperty("cateredFlag", out var catered) && catered.GetBoolean(),
                Brand = product.TryGetProperty("brand", out var brand) ? brand.GetString() : null,
                OriginStatement = product.TryGetProperty("originStatement", out var origin) ? origin.GetString() : null,
                LastUpdated = DateTime.UtcNow
            };

            var existing = await _db.NW_Product.FindAsync(entity.ProductId);
            if (existing == null)
                _db.NW_Product.Add(entity);
            else
                _db.Entry(existing).CurrentValues.SetValues(entity);
        }

        await _db.SaveChangesAsync();
    }

    public async Task SyncAllWoolworthsAsync()
    {
        var stores = await _db.WWStores.ToListAsync();

        foreach (var store in stores)
        {
            var categories = GetCategoryFieldNames(store.Extra1);
            foreach (var field in categories)
            {
                foreach (var value in GetCategoryValues(field))
                {
                    try
                    {
                        string json = await _api.PostProductsDynamicAsync("Woolworths", store.SiteID, field, value, 0);
                        // TODO: Deserialize and save to WW products table
                        _logger.LogInformation($"WW synced: {store.SiteID} / {field} / {value}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"WW sync failed: {store.SiteID} / {field} / {value}");
                    }
                }
            }
        }
    }

    private string[] GetCategoryFieldNames(string? extra1)
    {
        return extra1?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }

    private List<string> GetCategoryValues(string fieldName)
    {
        return fieldName switch
        {
            "category0SI" => new() { "Fruit & Veg", "Bakery", "Meat" },
            "category1SI" => new() { "Apples", "Bananas", "Lettuce" },
            _ => new() { "Other" }
        };
    }

    private async Task SavePnsProductsAsync(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var products = doc.RootElement.GetProperty("results");

        foreach (var product in products.EnumerateArray())
        {
            var entity = new PNS_Product
            {
                ProductId = product.GetProperty("productId").GetString() ?? Guid.NewGuid().ToString(),
                Name = product.GetProperty("name").GetString() ?? "",
                DisplayName = product.GetProperty("displayName").GetString() ?? "",
                SaleType = product.GetProperty("saleType").GetString() ?? "",
                RestrictedFlag = product.GetProperty("restrictedFlag").GetBoolean(),
                LiquorFlag = product.GetProperty("liquorFlag").GetBoolean(),
                TobaccoFlag = product.GetProperty("tobaccoFlag").GetBoolean(),
                OriginRegulated = product.GetProperty("originRegulated").GetBoolean(),
                CateredFlag = product.TryGetProperty("cateredFlag", out var catered) && catered.GetBoolean(),
                Brand = product.TryGetProperty("brand", out var brand) ? brand.GetString() : null,
                OriginStatement = product.TryGetProperty("originStatement", out var origin) ? origin.GetString() : null,
                LastUpdated = DateTime.UtcNow
            };

            // Upsert logic (simplified)
            var existing = await _db.PNS_Product.FindAsync(entity.ProductId);
            if (existing == null)
                _db.PNS_Product.Add(entity);
            else
                _db.Entry(existing).CurrentValues.SetValues(entity);
        }

        await _db.SaveChangesAsync();
    }

    private async Task SaveNewWorldProductsAsync(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var products = doc.RootElement.GetProperty("results");

        foreach (var product in products.EnumerateArray())
        {
            var entity = new NW_Product
            {
                ProductId = product.GetProperty("productId").GetString() ?? Guid.NewGuid().ToString(),
                Name = product.GetProperty("name").GetString() ?? "",
                DisplayName = product.GetProperty("displayName").GetString() ?? "",
                SaleType = product.GetProperty("saleType").GetString() ?? "",
                RestrictedFlag = product.GetProperty("restrictedFlag").GetBoolean(),
                LiquorFlag = product.GetProperty("liquorFlag").GetBoolean(),
                TobaccoFlag = product.GetProperty("tobaccoFlag").GetBoolean(),
                OriginRegulated = product.GetProperty("originRegulated").GetBoolean(),
                CateredFlag = product.TryGetProperty("cateredFlag", out var catered) && catered.GetBoolean(),
                Brand = product.TryGetProperty("brand", out var brand) ? brand.GetString() : null,
                OriginStatement = product.TryGetProperty("originStatement", out var origin) ? origin.GetString() : null,
                LastUpdated = DateTime.UtcNow
            };

            // Upsert logic
            var existing = await _db.NW_Product.FindAsync(entity.ProductId);
            if (existing == null)
                _db.NW_Product.Add(entity);
            else
                _db.Entry(existing).CurrentValues.SetValues(entity);
        }

        await _db.SaveChangesAsync();
    }

}
