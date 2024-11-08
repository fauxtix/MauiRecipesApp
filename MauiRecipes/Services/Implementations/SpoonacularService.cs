using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json;

namespace MauiRecipes.Services.Implementations;

public class SpoonacularService : ISpoonacularService
{
    private static HttpClient? _httpClient;
    private readonly string? _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a"; // get your own key from the site, this one will be removed shortly :)
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string baseAddress = "https://api.spoonacular.com/";

    public SpoonacularService()
    {

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private static HttpClient GetClient()
    {
        if (_httpClient is not null)
        {
            return _httpClient;
        }

        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        return _httpClient;
    }

    public async Task<CountriesCuisines.Root> GetRecipeTitles(string regionName, string ingredient = "", int number = 10)
    {
        var apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&query={ingredient}&cuisine={regionName}&number={number}";
        Uri uri = new Uri(apiQuery);
        try
        {
            var client = GetClient();
            HttpResponseMessage response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            CountriesCuisines.Root? output = JsonConvert.DeserializeObject<CountriesCuisines.Root>(responseString);

            return output ?? new();
        }

        catch (HttpRequestException e)
        {
            return new();
        }
        catch
        {
            return new(); // CountriesCuisines.Root();
        }
    }

    public async Task<List<Recipes.MyArray>> GetRecipeDetails(int id)
    {

        // https://api.spoonacular.com/recipes/324694/analyzedInstructions?stepBreakdown=true

        string url = $"{baseAddress}recipes/{id}/analyzedInstructions?apiKey={_apiKey}&language=pt";

        try
        {
            var client = GetClient();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipes.MyArray>>(responseString) ?? new List<Recipes.MyArray>();
        }

        catch (HttpRequestException e)
        {
            return new();
        }

        catch
        {
            return new(); // List<Recipes.MyArray>();
        }
    }

    public async Task<RecipeInformation.RecipeInfo> GetRecipeInformation(int id)
    {
        string url = $"{baseAddress}recipes/{id}/information?includeNutrition=false&apiKey={_apiKey}&language=pt";

        try
        {
            var client = GetClient();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RecipeInformation.RecipeInfo>(responseString) ?? new();
        }

        catch (HttpRequestException e)
        {
            return new();
        }

        catch
        {
            return new();
        }
    }
}
