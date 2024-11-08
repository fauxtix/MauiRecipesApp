namespace MauiRecipes.Services.Interfaces
{
    public interface IRecipeCacheService
    {
        Task SaveToCacheAsync<T>(string cacheKey, T data);
        Task<T?> LoadFromCacheAsync<T>(string cacheKey);
        void ClearCache();
    }
}