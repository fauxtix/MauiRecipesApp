﻿using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json;

namespace MauiRecipes.Services.Implementations;

public class SpoonacularService : ISpoonacularService
{
    private static HttpClient? _httpClient;
    private readonly string? _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a"; // get your own key from the site, this one will be removed shortly :)
    private readonly string baseAddress = "https://api.spoonacular.com/";
    private readonly JsonSerializerOptions _serializerOptions;

    private int _quotaLeft;
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

    public async Task<(double QuotaUsed, double QuotaLeft, double RequestCost)> GetQuotaDetailsAsync(string endpoint)
    {
        string testUrl = $"{baseAddress}{endpoint}?apiKey={_apiKey}&number=1";
        string endpointUrl = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&number=1";

        try
        {
            var client = GetClient();
            HttpResponseMessage response = await client.GetAsync(testUrl);

            // Ensure success and then retrieve the quota from response headers
            response.EnsureSuccessStatusCode();

            // Fetch the headers for Quota Used, Quota Left, and Request Cost
            var quotaUsedHeader = response.Headers.Contains("X-API-Quota-Used")
                ? response.Headers.GetValues("X-API-Quota-Used").FirstOrDefault()?.Trim()
                : "0";
            var quotaLeftHeader = response.Headers.Contains("X-API-Quota-Left")
                ? response.Headers.GetValues("X-API-Quota-Left").FirstOrDefault()?.Trim()
                : "0";
            var requestCostHeader = response.Headers.Contains("X-API-Quota-Request")
                ? response.Headers.GetValues("X-API-Quota-Request").FirstOrDefault()?.Trim()
                : "0";

            return (
                  QuotaUsed: double.TryParse(quotaUsedHeader, NumberStyles.Any, CultureInfo.InvariantCulture, out double quotaUsed) ? quotaUsed : 0,
                  QuotaLeft: double.TryParse(quotaLeftHeader, NumberStyles.Any, CultureInfo.InvariantCulture, out double quotaLeft) ? quotaLeft : 0,
                  RequestCost: double.TryParse(requestCostHeader, NumberStyles.Any, CultureInfo.InvariantCulture, out double requestCost) ? requestCost : 0
              );
        }
        catch (Exception ex)
        {
            // If there's an issue (like no quota header), return (-1, -1, -1) as an indicator of failure
            return (-1, -1, -1);
        }
    }

    public async Task<CountriesCuisines.Root> GetRecipeTitles(string regionName, string ingredient = "", int number = 10)
    {
        string apiQuery = string.Empty;
        if (string.IsNullOrEmpty(regionName))
        {
            // no region to search for, get random recipes from the api
            apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&query={ingredient}&cuisine={regionName}&number={number}&sort=random";
        }
        else
        {
            apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&query={ingredient}&cuisine={regionName}&number={number}";
        }

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

    public async Task<ApiResponse> GetPopularRecipesAsync(string region = "", string ingredient = "", int numberOfRecipes = 10)
    {
        var apiQuery = $"{baseAddress}recipes/complexSearch?apiKey={_apiKey}&query={ingredient}&cuisine={region}&number={numberOfRecipes}&sort=rating&number={numberOfRecipes}&addRecipeInformation=true";
        Uri uri = new Uri(apiQuery);

        try
        {
            var client = GetClient();
            HttpResponseMessage response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ApiResponse>(responseString);

            return data ?? new();

        }
        catch (Exception ex)
        {
            var errorMsg = ex.Message;
            return new();
        }
    }
    public async Task<RecipeInformation.RecipeInfo> GetRecipeInformation(int id)
    {

        string url = $"{baseAddress}recipes/{id}/information?includeNutrition=false&apiKey={_apiKey}";

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
