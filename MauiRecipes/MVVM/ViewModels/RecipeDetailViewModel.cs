using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using System.Collections.ObjectModel;

namespace MauiRecipes.MVVM.ViewModels
{

    [QueryProperty(nameof(RecipeInformation.RecipeInfo), "RecipeInfo")]

    public partial class RecipeDetailViewModel : ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        bool isBusy;

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
        private double amount;

        [ObservableProperty]
        private string? instructionName;
        [ObservableProperty]
        private string? instructionStepText;

        public RecipeDetailViewModel()
        {
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var data = query[nameof(RecipeInformation.RecipeInfo)] as RecipeInformation.RecipeInfo;
            RecipeInfo = data;
            Summary = RecipeInfo?.summary;
            RecipeImage = RecipeInfo?.image;

            // Clear previous data to prevent leftover info from showing
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
                            StepsList?.Add(step);  // Add steps one by one
                        }
                    }
                }
            }
        }
        [RelayCommand]
        public void GoBack()
        {
            IsBusy = true;
            Shell.Current.GoToAsync("//RecipesMainPage");
            IsBusy = false;
        }
    }
}
