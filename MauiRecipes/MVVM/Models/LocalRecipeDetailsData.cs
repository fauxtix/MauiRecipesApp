using SQLite;

namespace MauiRecipes.MVVM.Models
{
    public class LocalRecipeDetailsData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RecipeId { get; set; }
        public string? JsonData { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsFavorite { get; set; } = false;
    }
}
