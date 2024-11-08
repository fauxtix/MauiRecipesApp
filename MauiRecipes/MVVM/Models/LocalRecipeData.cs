using SQLite;

namespace MauiRecipes.MVVM.Models
{
    public class LocalRecipeData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string? RecipeKey { get; set; }

        public string? JsonData { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}

