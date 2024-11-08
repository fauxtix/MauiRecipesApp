using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using SQLite;
using System.Text.Json;

public class RecipeStorageService : IRecipeStorageService
{
    private readonly SQLiteAsyncConnection _connection;

    public RecipeStorageService(string dbPath)
    {
        var exist = File.Exists(dbPath); ;
        _connection = new SQLiteAsyncConnection(dbPath);
        if (!exist)
            _connection.CreateTableAsync<LocalRecipeData>().Wait();
        else
        {
            test();
        }

    }

    async void test()
    {
        var test = await _connection.Table<LocalRecipeData>()
                          .ToListAsync();

    }

    public async Task SaveToStorageAsync<T>(string recipeKey, T data)
    {
        var recipeData = new LocalRecipeData
        {
            RecipeKey = recipeKey,
            JsonData = JsonSerializer.Serialize(data),
            ExpirationDate = DateTime.UtcNow.AddDays(3)
        };

        await _connection.InsertAsync(recipeData);
    }

    public async Task<T?> LoadFromStorageAsync<T>(string recipeKey)
    {
        var recipeData = await _connection.Table<LocalRecipeData>()
                                          .FirstOrDefaultAsync(r => r.RecipeKey == recipeKey);
        return recipeData != null ? JsonSerializer.Deserialize<T>(recipeData.JsonData) : default;
    }

    public async Task ClearExpiredDataAsync()
    {
        var expiredRecipes = await _connection.Table<LocalRecipeData>()
                                              .Where(r => r.ExpirationDate.Date < DateTime.UtcNow.Date)
                                              .ToListAsync();
        foreach (var recipe in expiredRecipes)
        {
            await _connection.DeleteAsync(recipe);
        }
    }
}
