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
            _connection.CreateTableAsync<SavedSearches>().Wait();
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

    public async Task SaveDetailToStorageAsync<T>(int recipeId, T data, bool isFavorite = false)
    {
        var recipeData = await _connection.Table<LocalRecipeDetailsData>()
                                           .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        if (recipeData != null)
        {
            // Update existing record
            recipeData.JsonData = JsonSerializer.Serialize(data);
            recipeData.ExpirationDate = DateTime.Now.Date;
            recipeData.IsFavorite = isFavorite;
            await _connection.UpdateAsync(recipeData);
        }
        else
        {
            // Insert new record
            recipeData = new LocalRecipeDetailsData
            {
                RecipeId = recipeId,
                JsonData = JsonSerializer.Serialize(data),
                ExpirationDate = DateTime.Now.Date,
                IsFavorite = isFavorite
            };
            await _connection.InsertAsync(recipeData);
        }
    }

    public async Task MarkAsFavoriteAsync(int recipeId, bool isFavorite)
    {
        var recipeData = await _connection.Table<LocalRecipeDetailsData>()
                                           .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        if (recipeData != null)
        {
            recipeData.IsFavorite = isFavorite;
            await _connection.UpdateAsync(recipeData);
        }
    }

    public async Task<T?> LoadFromStorageAsync<T>(string recipeKey) // Titles
    {
        var recipeData = await _connection.Table<LocalRecipeData>()
                                          .FirstOrDefaultAsync(r => r.RecipeKey == recipeKey);
        var output = recipeData != null ? JsonSerializer.Deserialize<T>(recipeData.JsonData!) : default;
        return output;
    }
    public async Task<(T? Recipe, bool IsFavorite)> LoadDetailFromStorageAsync<T>(int recipeId)
    {
        var recipeDetailData = await _connection.Table<LocalRecipeDetailsData>()
                                                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

        var recipe = recipeDetailData != null
                     ? JsonSerializer.Deserialize<T>(recipeDetailData.JsonData!)
                     : default;

        bool isFavorite = recipeDetailData?.IsFavorite ?? false;

        return (recipe, isFavorite);
    }


    public async Task<List<T>> GetAllFavoritesAsync<T>()
    {
        var favoriteRecipes = await _connection.Table<LocalRecipeDetailsData>()
                                               .Where(r => r.IsFavorite)
                                               .ToListAsync();
        var output = favoriteRecipes
               .Select(f => JsonSerializer.Deserialize<T>(f.JsonData!)!)
               .ToList();
        return output;
    }

    public async Task<T?> LoadFavoriteRecipeAsync<T>(int recipeId)
    {
        // Query the database for a single favorite recipe by ID
        var favoriteRecipe = await _connection.Table<LocalRecipeDetailsData>()
                                              .Where(r => r.IsFavorite && r.RecipeId == recipeId)
                                              .FirstOrDefaultAsync();

        var output = favoriteRecipe != null ? JsonSerializer.Deserialize<T>(favoriteRecipe.JsonData!) : default;

        return output;
    }

    public async Task SaveSearch(string region, string ingredient, int numberOfRecipes)
    {
        SavedSearches searchToStore =
            new()
            {
                Ingredient = ingredient,
                Region = region,
                SaveDate = DateTime.Now,
                NumberOfRecipes = numberOfRecipes
            };
        await _connection.InsertAsync(searchToStore);
    }

    public async Task<List<SavedSearches>> GetSavedSearches()
    {
        var savedSearches = await _connection.Table<SavedSearches>()
            .ToListAsync();
        savedSearches = savedSearches.OrderByDescending(s => s.SaveDate).Take(6).ToList();

        return savedSearches;
    }

    public async Task<SavedSearches> GetSavedSearchById(int id)
    {
        var savedSearch = await _connection.Table<SavedSearches>()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();

        return savedSearch;
    }

    public async Task<List<T>> GetRecipesDetailsStored<T>()
    {
        var storedRecipesDetails = await _connection.Table<LocalRecipeDetailsData>()
            .ToListAsync();

        storedRecipesDetails = storedRecipesDetails.OrderByDescending(s => s.RecipeId).Take(10).ToList();

        var output = storedRecipesDetails
               .Select(f => JsonSerializer.Deserialize<T>(f.JsonData!)!)
               .ToList();
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
        catch
        {
            throw;
        }
    }
}
