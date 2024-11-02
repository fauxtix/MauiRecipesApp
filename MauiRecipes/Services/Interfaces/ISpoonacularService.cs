using MauiRecipes.MVVM.Models;

namespace MauiRecipes.Services.Interfaces;
public interface ISpoonacularService
{
    Task<List<Recipes.MyArray>> GetRecipeDetails(int Id);
    Task<RecipeInformation.RecipeInfo> GetRecipeInformation(int Id);
    Task<CountriesCuisines.Root> GetRecipeTitles(string regionName, string ingredient = "", int number = 10);
}
