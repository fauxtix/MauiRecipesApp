using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using SQLite;
using System.Text.Json;

public class RecipeStorageService : IRecipeStorageService
{
    private readonly SQLiteAsyncConnection _connection;

    public RecipeStorageService(string dbPath)
    {
        var dbExist = File.Exists(dbPath); ;
        _connection = new SQLiteAsyncConnection(dbPath);

        if (!dbExist)
        {
            _connection.CreateTableAsync<LocalRecipeData>().Wait();
            _connection.CreateTableAsync<LocalRecipeDetailsData>().Wait();
        }
    }

    public async Task SaveToStorageAsync<T>(string recipeKey, T data) // Titles
    {
        var recipeData = new LocalRecipeData
        {
            RecipeKey = recipeKey,
            JsonData = JsonSerializer.Serialize(data),
            ExpirationDate = DateTime.Now.Date
        };

        await _connection.InsertAsync(recipeData);
    }

    public async Task SaveDetailToStorageAsync<T>(int recipeId, T data) // Detail
    {
        var recipeData = new LocalRecipeDetailsData
        {
            RecipeId = recipeId,
            JsonData = JsonSerializer.Serialize(data),
            ExpirationDate = DateTime.Now.Date
        };

        await _connection.InsertAsync(recipeData);
    }


    public async Task<T?> LoadFromStorageAsync<T>(string recipeKey) // Titles
    {
        var recipeData = await _connection.Table<LocalRecipeData>()
                                          .FirstOrDefaultAsync(r => r.RecipeKey == recipeKey);
        var output = recipeData != null ? JsonSerializer.Deserialize<T>(recipeData.JsonData!) : default;
        return output;
    }
    public async Task<T?> LoadDetailFromStorageAsync<T>(int recipeId) // Detail
    {
        var recipeDetailData = await _connection.Table<LocalRecipeDetailsData>()
                                          .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        var output = recipeDetailData != null ? JsonSerializer.Deserialize<T>(recipeDetailData.JsonData!) : default;
        return output;
    }

    public async Task ClearExpiredDataAsync()
    {
        try
        {
            var allRecipes = await _connection.Table<LocalRecipeData>()
                .ToListAsync();

            foreach (var recipe in allRecipes)
            {
                await _connection.DeleteAsync(recipe);
            }

            var allRecipesDetails = await _connection.Table<LocalRecipeDetailsData>()
                                                    .ToListAsync();

            foreach (var detail in allRecipesDetails)
            {
                await _connection.DeleteAsync(detail);
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
