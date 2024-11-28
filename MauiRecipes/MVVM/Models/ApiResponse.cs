using Newtonsoft.Json;
using static MauiRecipes.MVVM.Models.RecipeInformation;

namespace MauiRecipes.MVVM.Models
{
    public class ApiResponse
    {
        [JsonProperty("results")]
        public List<RecipeInfo> Results { get; set; } = new();
    }
}
