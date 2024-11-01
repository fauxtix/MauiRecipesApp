﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Font = Microsoft.Maui.Font;

namespace MauiRecipes.MVVM.ViewModels;
public partial class SpoonacularViewModel : BaseViewModel
{
    private readonly ISpoonacularService? _service;

    [ObservableProperty]
    private CountriesCuisines.Root? titles;
    [ObservableProperty]
    private List<Recipes.MyArray>? recipeDetail;

    [ObservableProperty]
    private RecipeInformation.RecipeInfo? recipeInfo;

    [ObservableProperty]
    private string? recipient;

    public ObservableCollection<CountriesCuisines.Result?> RecipesTitles { get; } = new();
    public ObservableCollection<Recipes.MyArray?> RecipeDetails { get; } = new();


    public SpoonacularViewModel(ISpoonacularService service)
    {

        if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
        {
            ShowInfoOrAlert(Colors.Red, Colors.White, "No internet access");
            return;
        }


        _service = service;
        IsBusy = true;
        PropertyChanged += SpoonacularViewModel_PropertyChanged;
        GetRecipesTitles();
        regionsData =
        [
            new() { ID= "German", RegionName = "Alemã" },
            new() { ID= "American", RegionName= "Americana" },
            new() { ID= "Latin American", RegionName = "América Latina" },
            new() { ID= "British", RegionName= "Britânica" },
            new() { ID= "Cajun", RegionName= "Cajun" },
            new() { ID= "Caribbean", RegionName= "Caraíbas" },
            new() { ID= "Korean", RegionName = "Coreana" },
            new() { ID= "Spanish", RegionName = "Espanhola"},
            new() { ID= "Eastern European", RegionName= "Europa de Leste" },
            new() { ID= "French", RegionName = "Francesa" },
            new() { ID= "Greek", RegionName = "Grega"},
            new() { ID= "Indian", RegionName = "Indiana" },
            new() { ID= "Irish", RegionName = "Irlandesa"},
            new() { ID= "Italian", RegionName = "Italiana" },
            new() { ID= "Japanese", RegionName = "Japonesa" },
            new() { ID= "Jewish", RegionName = "Judeia" },
            new() { ID= "Mediterranean", RegionName = "Mediterrânica" },
            new() { ID= "Mexican", RegionName = "Mexicana" },
            new() { ID= "Middle Eastern", RegionName = "Médio Oriente"},
            new() { ID= "Nordic", RegionName = "Nórdica" },
            new() { ID= "Southern", RegionName = "Sulista"},
            new() { ID= "Thai", RegionName = "Thai"},
            new() { ID= "Vietnamese", RegionName= "Vietnamita"}
        ];

        Regions.Clear();
        foreach (var region in regionsData)
        {
            Regions.Add(region);
        }

        SelectedRegion = Regions.FirstOrDefault(r => r.ID == RegionToFilter);
        IsBusy = false;
    }

    public async void GetRecipesTitles()
    {
        IsBusy = true;
        await Task.Delay(200);
        Titles = await _service!.GetRecipeTitles(RegionToFilter, Recipient!);
        RecipesTitles.Clear();
        foreach (var recipe in Titles.results)
        {
            RecipesTitles.Add(recipe);
        }
        IsBusy = false;
        ShowInfoOrAlert(Colors.BlueViolet, Colors.White, $"Recipes loaded for Region '{RegionToFilter}'", 2);
    }

    [RelayCommand]
    private void SetRecipientToSearch(string searchText)
    {
        Recipient = searchText;
        GetRecipesTitles();
    }


    [RelayCommand]
    private async Task GetRecipeDetails(CountriesCuisines.Result param)
    {
        RecipeDetail = await _service!.GetRecipeDetails(param.Id);
    }

    [RelayCommand]
    private async Task GetRecipeInformation(CountriesCuisines.Result param)
    {
        RecipeInfo = await _service!.GetRecipeInformation(param.Id);

        try
        {
            IsBusy = true;
            await Task.Delay(200);
            await Shell.Current.GoToAsync($"//{nameof(ViewRecipePage)}", true,
                new Dictionary<string, object>
                {
                    {"RecipeInfo", RecipeInfo },
                 });
        }
        catch (Exception ex)
        {
            ShowInfoOrAlert(Colors.Red, Colors.White, $"{ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }

    }

    private void SpoonacularViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedRegion) && SelectedRegion != null)
        {
            RegionToFilter = SelectedRegion.ID;
            GetRecipesTitles();
        }
    }

    private async void ShowInfoOrAlert(Color backgroundColor, Color textColor, string alertMessage = "", int durationInSeconds = 5)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = backgroundColor,
            TextColor = textColor,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(12),
            ActionButtonFont = Font.SystemFontOfSize(12),
            CharacterSpacing = 0.2
        };

        TimeSpan duration = TimeSpan.FromSeconds(durationInSeconds);
        var snackbar = Snackbar.Make(alertMessage, null, "Ok", duration, snackbarOptions);
        await snackbar.Show(cancellationTokenSource.Token);
    }
}
