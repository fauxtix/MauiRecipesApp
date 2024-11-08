using MauiRecipes.Services.Interfaces;
using System.Text.Json;
namespace MauiRecipes.Services.Implementations
{
    public class RecipeCacheService : IRecipeCacheService
    {
        private readonly string cacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RecipeCache");

        public RecipeCacheService()
        {
            if (!Directory.Exists(cacheDirectory))
            {
                Directory.CreateDirectory(cacheDirectory);
            }
        }

        public async Task SaveToCacheAsync<T>(string cacheKey, T data)
        {
            string filePath = GetCacheFilePath(cacheKey);
            string jsonData = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(filePath, jsonData);
        }

        public async Task<T?> LoadFromCacheAsync<T>(string cacheKey)
        {
            string filePath = GetCacheFilePath(cacheKey);

            if (File.Exists(filePath))
            {
                string jsonData = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<T>(jsonData);
            }

            return default;
        }

        public void ClearCache()
        {
            if (Directory.Exists(cacheDirectory))
            {
                Directory.Delete(cacheDirectory, true);
                Directory.CreateDirectory(cacheDirectory);
            }
        }

        private string GetCacheFilePath(string cacheKey) => Path.Combine(cacheDirectory, $"{cacheKey}.json");
    }
}
