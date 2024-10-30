using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;

namespace MauiRecipes.MVVM.ViewModels
{

    [QueryProperty(nameof(RecipeInformation.RecipeInfo), "RecipeInfo")]

    public partial class RecipeDetailViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private RecipeInformation.RecipeInfo? recipeInfo;

        [ObservableProperty]
        private string? summary;
        [ObservableProperty]
        private string? recipeImage;
        public RecipeDetailViewModel()
        {
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var data = query[nameof(RecipeInformation.RecipeInfo)] as RecipeInformation.RecipeInfo;
            RecipeInfo = data;
            Summary = RecipeInfo?.summary;
            RecipeImage = RecipeInfo?.image;
        }

        [RelayCommand]
        public void GoBack()
        {
            Shell.Current.GoToAsync("//RecipesMainPage");
        }
    }
}
