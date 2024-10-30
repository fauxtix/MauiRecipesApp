namespace MauiRecipes.MVVM.Models;

public class RecipeWithDetails : CountriesCuisines.Result
{
    public required RecipeInformation.RecipeInfo Details { get; set; }
}
