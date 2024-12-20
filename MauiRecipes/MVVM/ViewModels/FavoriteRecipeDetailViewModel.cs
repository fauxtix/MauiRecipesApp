﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using System.Collections.ObjectModel;

namespace MauiRecipes.MVVM.ViewModels
{
    [QueryProperty(nameof(FavoritesData), "FavoritesData")]

    public partial class FavoriteRecipeDetailViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        private FavoritesData? favoriteRecord;

        [ObservableProperty]
        private FavoritesData? recipeInfo;

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
        private string? instructionName;
        [ObservableProperty]
        private string? instructionStepText;


        public FavoriteRecipeDetailViewModel()
        {
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            IsBusy = true;
            await Task.Yield();
            var data = query[nameof(FavoritesData)] as FavoritesData;
            RecipeInfo = data;
            Summary = RecipeInfo?.summary;
            RecipeImage = RecipeInfo?.Image;
            Title = RecipeInfo?.Title;

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

            IsBusy = false;
        }

        [RelayCommand]
        public async Task GoBack()
        {
            IsBusy = true;
            await Task.Yield();
            await Shell.Current.GoToAsync($"{nameof(FavoritesPage)}");
            IsBusy = false;
        }

    }
}
