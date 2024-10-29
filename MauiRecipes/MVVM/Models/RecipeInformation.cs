namespace MauiRecipes.MVVM.Models
{
    public class RecipeInformation
    {
        public class Us
        {
            public double amount { get; set; }
            public string unitShort { get; set; }
            public string unitLong { get; set; }
        }

        public class Metric
        {
            public double amount { get; set; }
            public string unitShort { get; set; }
            public string unitLong { get; set; }
        }

        public class Measures
        {
            public Us us { get; set; }
            public Metric metric { get; set; }
        }

        public class ExtendedIngredient
        {
            public int id { get; set; }
            public string aisle { get; set; }
            public string image { get; set; }
            public string consistency { get; set; }
            public string name { get; set; }
            public string original { get; set; }
            public string originalString { get; set; }
            public string originalName { get; set; }
            public double amount { get; set; }
            public string unit { get; set; }
            public IList<string> meta { get; set; }
            public IList<string> metaInformation { get; set; }
            public Measures measures { get; set; }
        }

        public class WinePairing
        {
        }

        public class Ingredient
        {
            public int id { get; set; }
            public string name { get; set; }
            public string localizedName { get; set; }
            public string image { get; set; }
        }

        public class Temperature
        {
            public double number { get; set; }
            public string unit { get; set; }
        }

        public class Equipment
        {
            public int id { get; set; }
            public string name { get; set; }
            public string localizedName { get; set; }
            public string image { get; set; }
            public Temperature temperature { get; set; }
        }

        public class Length
        {
            public int number { get; set; }
            public string unit { get; set; }
        }

        public class Step
        {
            public int number { get; set; }
            public string step { get; set; }
            public IList<Ingredient> ingredients { get; set; }
            public IList<Equipment> equipment { get; set; }
            public Length length { get; set; }
        }

        public class AnalyzedInstruction
        {
            public string name { get; set; }
            public IList<Step> steps { get; set; }
        }

        public class RecipeInfo
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
            public IList<ExtendedIngredient> extendedIngredients { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public int readyInMinutes { get; set; }
            public int servings { get; set; }
            public string sourceUrl { get; set; }
            public string image { get; set; }
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
}
