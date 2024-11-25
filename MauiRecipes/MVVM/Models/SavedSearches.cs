using SQLite;

namespace MauiRecipes.MVVM.Models
{
    public class SavedSearches
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime SaveDate { get; set; }
        [Indexed]
        public string? Region { get; set; }
        [Indexed]
        public string? Ingredient { get; set; }
        public int NumberOfRecipes { get; set; }
    }
}
