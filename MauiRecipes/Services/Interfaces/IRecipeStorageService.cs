namespace MauiRecipes.Services.Interfaces;

public interface IRecipeStorageService
{
    Task SaveToStorageAsync<T>(string recipeKey, T data);
    Task<T?> LoadFromStorageAsync<T>(string recipeKey);
    Task ClearExpiredDataAsync();
    Task SaveDetailToStorageAsync<T>(int recipeId, T data);
    Task<T?> LoadDetailFromStorageAsync<T>(int recipeId);
}
