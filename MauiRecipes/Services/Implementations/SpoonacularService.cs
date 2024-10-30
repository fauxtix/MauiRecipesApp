using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json;

namespace MauiRecipes.Services.Implementations;

public class SpoonacularService : ISpoonacularService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a"; // create your own, this key will be removed shortly :)
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string baseAddress = "https://api.spoonacular.com/";

    public SpoonacularService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    // Método para obter títulos de receitas
    public async Task<CountriesCuisines.Root> GetRecipeTitles(string regionName)
    {
        var apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&cuisine={regionName}";
        Uri uri = new Uri(apiQuery);
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            CountriesCuisines.Root? output = JsonConvert.DeserializeObject<CountriesCuisines.Root>(responseString);
            return output ?? new();
        }
        catch (Exception ex)
        {
            return new(); // CountriesCuisines.Root();
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
        catch (Exception)
        {
            return new(); // List<Recipes.MyArray>();
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
            return JsonConvert.DeserializeObject<RecipeInformation.RecipeInfo>(responseString) ?? new();
        }
        catch (Exception)
        {
            return new();
        }
    }
}
