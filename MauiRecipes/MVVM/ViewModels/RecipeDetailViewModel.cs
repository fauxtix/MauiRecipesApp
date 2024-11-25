using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;

namespace MauiRecipes.MVVM.ViewModels
{

    [QueryProperty(nameof(RecipeInformation.RecipeInfo), "RecipeInfo")]
    [QueryProperty(nameof(IsFavorite), "IsFavorite")]

    public partial class RecipeDetailViewModel : BaseViewModel, IQueryAttributable
    {

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        private FavoritesData? favoriteRecord;
        [ObservableProperty]
        private RecipeInformation.RecipeInfo? recipeInfo;
        [ObservableProperty]
        private List<RecipeInformation.ExtendedIngredient>? ingredients = new();
        [ObservableProperty]
        private List<RecipeInformation.AnalyzedInstruction>? instructions = new();

        [ObservableProperty]
        public ObservableCollection<RecipeInformation.Step>? stepsList = new();

        [ObservableProperty]
        private string? summary;
        [ObservableProperty]
        private string? recipeImage;
        [ObservableProperty]
        private string? image;
        [ObservableProperty]
        private string? original;
        [ObservableProperty]
        private string? originalName;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private string? ingredientImage;

        [ObservableProperty]
        private bool isFavorite = false;

        [ObservableProperty]
        private double amount;

        [ObservableProperty]
        private string? instructionName;
        [ObservableProperty]
        private string? instructionStepText;

        private readonly IRecipeStorageService _recipeStorageService;
        public RecipeDetailViewModel(IRecipeStorageService recipeStorageService)
        {
            _recipeStorageService = recipeStorageService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var data = query[nameof(RecipeInformation.RecipeInfo)] as RecipeInformation.RecipeInfo;
            IsFavorite = (bool)query[nameof(IsFavorite)];
            RecipeInfo = data;
            Summary = RecipeInfo?.summary;
            RecipeImage = RecipeInfo?.Image;

            Ingredients?.Clear();
            Instructions?.Clear();
            StepsList?.Clear();

            if (RecipeInfo?.extendedIngredients?.Count > 0)
            {
                Ingredients = RecipeInfo.extendedIngredients.ToList();
            }

            if (RecipeInfo?.analyzedInstructions?.Count > 0)
            {
                Instructions = RecipeInfo.analyzedInstructions.ToList();

                foreach (var instruction in RecipeInfo.analyzedInstructions)
                {
                    if (instruction.Steps != null)
                    {
                        foreach (var step in instruction.Steps)
                        {
                            StepsList?.Add(step);
                        }
                    }
                }
            }
        }

        [RelayCommand]
        public async Task ToggleFavorite()
        {
            if (RecipeInfo == null) return;

            IsFavorite = !IsFavorite;

            //UpdateToolbarIcon();

            await _recipeStorageService.SaveDetailToStorageAsync(RecipeInfo.id, RecipeInfo, IsFavorite);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }

        private void UpdateToolbarIcon()
        {
            var page = Shell.Current.CurrentPage as ViewRecipePage;
            if (page != null)
            {
                var toolbarItem = page.ToolbarItems.FirstOrDefault();
                if (toolbarItem != null)
                {
                    toolbarItem.IconImageSource = IsFavorite ? "favorite_filled.png" : "favorite_outline.png";
                }
            }
        }

    }
}
