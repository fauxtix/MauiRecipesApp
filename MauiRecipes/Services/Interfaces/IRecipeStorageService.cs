
using MauiRecipes.MVVM.Models;

namespace MauiRecipes.Services.Interfaces;

public interface IRecipeStorageService
{
    Task SaveToStorageAsync<T>(string recipeKey, T data);
    Task<T?> LoadFromStorageAsync<T>(string recipeKey);
    Task ClearExpiredDataAsync();
    Task SaveDetailToStorageAsync<T>(int recipeId, T data, bool isFavorite = false);
    Task<(T? Recipe, bool IsFavorite)> LoadDetailFromStorageAsync<T>(int recipeId);
    Task MarkAsFavoriteAsync(int recipeId, bool isFavorite);
    Task<List<T>> GetAllFavoritesAsync<T>();
    Task<T?> LoadFavoriteRecipeAsync<T>(int recipeId);
    Task SaveSearch(string region, string ingredient, int numberOfRecipes);
    Task<List<SavedSearches>> GetSavedSearches();
}
