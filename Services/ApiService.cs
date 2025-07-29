using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly SecretClient _secretClient;

    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        var vaultUri = new Uri(_config["KeyVault:VaultUri"]);
        _secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());
    }

    private async Task<string> GetBearerTokenAsync()
    {
        string secretName = _config["KeyVault:TokenSecretName"];
        KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
        return secret.Value;
    }

    public async Task<List<JsonElement>> PostProductsDynamicAsync(string retailer, string storeId, string categoryFieldName, string categoryValue, int startPage = 0, int maxPages = 10)
    {
        string token = await GetBearerTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        string baseUrl = _config[$"RetailApi:{retailer}:BaseUrl"];
        string endpoint = _config[$"RetailApi:{retailer}:Endpoints:Products"];

        var allProducts = new List<JsonElement>();

        for (int page = startPage; page < startPage + maxPages; page++)
        {
            string filters = $"stores:{storeId} AND {categoryFieldName}:\\\"{categoryValue}\\\"";

            var payload = new Dictionary<string, object>
            {
                ["algoliaQuery"] = new Dictionary<string, object>
                {
                    ["attributesToHighlight"] = Array.Empty<string>(),
                    ["attributesToRetrieve"] = new[] { "productID", "Type", "sponsored", "category0SI", "category1SI", "category2SI" },
                    ["facets"] = new[] { "brand", "category1SI", "onPromotion", "productFacets", "tobacco" },
                    ["filters"] = filters,
                    ["highlightPostTag"] = "__/ais-highlight__",
                    ["highlightPreTag"] = "__ais-highlight__",
                    ["hitsPerPage"] = 50,
                    ["maxValuesPerFacet"] = 100,
                    ["page"] = page,
                    ["analyticsTags"] = new[] { "fs#WEB:desktop" }
                },
                ["algoliaFacetQueries"] = new object[] { },
                ["storeId"] = storeId,
                ["hitsPerPage"] = 50,
                ["page"] = page,
                ["sortOrder"] = "SI_POPULARITY_ASC",
                ["tobaccoQuery"] = false,
                ["precisionMedia"] = new Dictionary<string, object>
                {
                    ["adDomain"] = "CATEGORY_PAGE",
                    ["adPositions"] = new[] { 4, 8, 12, 16 },
                    ["publishImpressionEvent"] = false,
                    ["disableAds"] = true
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + endpoint, content);
            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            if (!doc.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                break;

            allProducts.AddRange(results.EnumerateArray());
        }

        return allProducts;
    }

}
