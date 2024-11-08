using SQLite;

namespace MauiRecipes.MVVM.Models
{
    public class LocalRecipeData
    {
        [PrimaryKey]
        public string? CacheKey { get; set; }
        public string? JsonData { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
