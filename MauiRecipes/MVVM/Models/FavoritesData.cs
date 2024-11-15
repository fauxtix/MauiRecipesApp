using static MauiRecipes.MVVM.Models.RecipeInformation;

namespace MauiRecipes.MVVM.Models
{
    public class FavoritesData
    {
        public bool vegetarian { get; set; }
        public bool vegan { get; set; }
        public bool glutenFree { get; set; }
        public bool dairyFree { get; set; }
        public bool veryHealthy { get; set; }
        public bool cheap { get; set; }
        public bool veryPopular { get; set; }
        public bool sustainable { get; set; }
        public int weightWatcherSmartPoints { get; set; }
        public string gaps { get; set; }
        public bool lowFodmap { get; set; }
        public int aggregateLikes { get; set; }
        public double spoonacularScore { get; set; }
        public double healthScore { get; set; }
        public double pricePerServing { get; set; }
        public IList<ExtendedIngredient>? extendedIngredients { get; set; }
        public int id { get; set; }
        public string? Title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }

        public string sourceUrl { get; set; }
        public string? Image { get; set; }
        public string imageType { get; set; }
        public string summary { get; set; }
        public IList<string> cuisines { get; set; }
        public IList<string> dishTypes { get; set; }
        public IList<string> diets { get; set; }
        public IList<object> occasions { get; set; }
        public WinePairing winePairing { get; set; }
        public string instructions { get; set; }
        public IList<AnalyzedInstruction> analyzedInstructions { get; set; }
        public object sourceName { get; set; }
        public object creditsText { get; set; }
        public object originalId { get; set; }
    }
}
