using MauiRecipes.MVVM.Models;

namespace MauiRecipes.Services.Interfaces;
public interface ISpoonacularService
{
    Task<RecipeInformation.RecipeInfo> GetRecipeInformation(int Id);
    Task<CountriesCuisines.Root> GetRecipeTitles(string regionName, string ingredient = "", int number = 10);
    Task<(double QuotaUsed, double QuotaLeft, double RequestCost)> GetQuotaDetailsAsync(string endpointUrl);
}
