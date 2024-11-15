namespace MauiRecipes.MVVM.Models
{
    public class SavedSearches
    {
        public int Id { get; set; }
        public DateTime SaveDate { get; set; }
        public string? Region { get; set; }
        public string? Ingredient { get; set; }
        public int NumberOfRecipes { get; set; }
    }
}
