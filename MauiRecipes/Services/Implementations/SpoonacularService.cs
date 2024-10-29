using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace MauiRecipes.Services.Implementations;

public class SpoonacularService : ISpoonacularService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SpoonacularService> _logger;
    private readonly string _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a";
    private readonly string _apiKey1 = "68844774cab14f24986294fe1ccc6a4e";
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string baseAddress = "https://api.spoonacular.com/";

    public SpoonacularService(ILogger<SpoonacularService> logger)
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        _logger = logger;
    }

    // Método para obter títulos de receitas
    public async Task<CountriesCuisines.Root> GetRecipeTitles(string regionName)
    {
        var apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&cuisine={regionName}";
        Uri uri = new Uri(apiQuery);
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            CountriesCuisines.Root? output = JsonConvert.DeserializeObject<CountriesCuisines.Root>(responseString);
            return output ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching recipe titles: {ex.Message}");
            return new CountriesCuisines.Root();
        }
    }

    // Método para obter detalhes de uma receita
    public async Task<List<Recipes.MyArray>> GetRecipeDetails(int id)
    {
        string url = $"{baseAddress}recipes/{id}/analyzedInstructions?apiKey={_apiKey}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipes.MyArray>>(responseString) ?? new List<Recipes.MyArray>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching recipe details: {ex.Message}");
            return new List<Recipes.MyArray>();
        }
    }

    // Método para obter informações detalhadas de uma receita
    public async Task<RecipeInformation.RecipeInfo> GetRecipeInformation(int id)
    {
        string url = $"{baseAddress}recipes/{id}/information?includeNutrition=false&apiKey={_apiKey}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RecipeInformation.RecipeInfo>(responseString) ?? new RecipeInformation.RecipeInfo();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching recipe information: {ex.Message}");
            return new RecipeInformation.RecipeInfo();
        }
    }
}
