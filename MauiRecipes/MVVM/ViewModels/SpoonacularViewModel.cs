using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;

namespace MauiRecipes.MVVM.ViewModels;
public partial class SpoonacularViewModel : BaseViewModel
{
    private readonly ISpoonacularService? _service;
    private readonly IRecipeStorageService? _storageService;
    private readonly IAlertService _alertService;

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

    [ObservableProperty]
    private bool isInitialLoadComplete;

    public SpoonacularViewModel(ISpoonacularService service,
        IRecipeStorageService storageService, IAlertService alertService)
    {
        _service = service;
        _storageService = storageService;
        _alertService = alertService;

        if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
        {
            ShowNetworkAlert("No internet access");
            return;
        }

        _service = service;

        PropertyChanged += SpoonacularViewModel_PropertyChanged!;

        InitializeRegions();

        GetFirstData();
    }

    private async void GetFirstData()
    {
        await GetRecipesTitles();
    }

    private void InitializeRegions()
    {
        RegionsData =
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

        RegionsData = RegionsData.OrderBy(o => o.ID).ToList();

        Regions.Clear();
        foreach (var region in RegionsData)
        {
            Regions.Add(region);
        }

        SelectedRegion = Regions.FirstOrDefault(r => r.ID == RegionToFilter);
        IsBusy = false;

    }

    [RelayCommand]
    public async Task GetRecipesTitles()
    {
        IsBusy = true;
        await Task.Yield();

        try
        {
            string region = !string.IsNullOrEmpty(RegionToFilter) ? RegionToFilter : "defaultRegion";
            string recipient = !string.IsNullOrEmpty(Recipient) ? Recipient : "defaultRecipient";
            string cacheKey = $"{region}_{recipient}_{NumberOfRecipes}";

            Titles = await LoadTitlesFromCacheAsync(cacheKey);

            if (Titles is not null && Titles.results is not null && Titles.results.Any())
            {
                AddRecipesToTitlesCollection();
                await ShowUserFeedbackAsync("Recipes loaded from database.", MessageType.Success, durationInSeconds: 2);
            }
            else
            {
                // Caso não haja dados no cache, chama a API e armazena no SQLite
                Titles = await _service!.GetRecipeTitles(RegionToFilter, Recipient!, NumberOfRecipes);

                AddRecipesToTitlesCollection();
                await AddTitlesToStorage(cacheKey);

                var userFeedback = GetUserFeedbackMessage();
                await ShowUserFeedbackAsync(userFeedback.message, userFeedback.type, durationInSeconds: 2);
            }
        }
        catch
        {
            await ShowUserFeedbackAsync($"Recipes failed to load for Region '{RegionToFilter}'", MessageType.Error, durationInSeconds: 5);
        }
        finally
        {
            IsBusy = false;
            IsInitialLoadComplete = true;
        }
    }

    [RelayCommand]
    private async Task SearchRecipes()
    {
        if (string.IsNullOrEmpty(Recipient) && string.IsNullOrEmpty(RegionToFilter))
            return;

        await GetRecipesTitles();
    }

    [RelayCommand]
    private async Task GetRecipeDetails(CountriesCuisines.Result param)
    {
        RecipeDetail = await _storageService!.LoadDetailFromStorageAsync<List<Recipes.MyArray>>(param.Id);

        if (RecipeDetail == null)
        {
            RecipeDetail = await _service!.GetRecipeDetails(param.Id);
            await _storageService.SaveDetailToStorageAsync(param.Id, RecipeDetail);

        }
    }

    [RelayCommand]
    private async Task GetRecipeInformation(CountriesCuisines.Result param)
    {
        IsBusy = true;
        await Task.Yield();

        try
        {
            var recipeInfo = await FetchRecipeInformationFromStorageOrApi(param.Id);

            if (recipeInfo == null)
            {
                await ShowUserFeedbackAsync("Failed to load recipe information.", MessageType.Error);
                return;
            }

            await ShowUserFeedbackAsync("Recipe detail loaded successfully.", MessageType.Success);

            await Shell.Current.GoToAsync($"//{nameof(ViewRecipePage)}", true, new Dictionary<string, object>
            {
                {"RecipeInfo", recipeInfo}
             });
        }
        catch (Exception ex)
        {
            await ShowUserFeedbackAsync($"Error: {ex.Message}", MessageType.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    public async Task ClearCacheAsync()
    {
        try
        {
            bool okToDelete = await Shell.Current.DisplayAlert("Please confirm", $"Delete ALL cached searches from database?", "Yes", "No");
            if (okToDelete)
            {
                IsBusy = true;
                await Task.Yield();
                await _storageService!.ClearExpiredDataAsync();
                await ShowUserFeedbackAsync("All cached searches removed from database.", MessageType.Info, durationInSeconds: 5); await GetRecipesTitles();
            }
        }
        catch (Exception ex)
        {
            await ShowUserFeedbackAsync($"Error removing recipes from cache {ex.Message}.", MessageType.Error, durationInSeconds: 5);
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
        }
    }

    private void AddRecipesToTitlesCollection()
    {
        RecipesTitles.Clear();
        foreach (var recipe in Titles!.results)
        {
            RecipesTitles.Add(recipe);
        }
    }


    private async Task<RecipeInformation.RecipeInfo> FetchRecipeInformationFromStorageOrApi(int recipeId)
    {
        var recipeInfo = await _storageService!.LoadDetailFromStorageAsync<RecipeInformation.RecipeInfo>(recipeId);
        if (recipeInfo == null)
        {
            recipeInfo = await _service!.GetRecipeInformation(recipeId);
            await _storageService.SaveDetailToStorageAsync(recipeId, recipeInfo);
        }
        return recipeInfo;
    }

    private async Task<CountriesCuisines.Root> LoadTitlesFromCacheAsync(string cacheKey)
    {
        return await _storageService!.LoadFromStorageAsync<CountriesCuisines.Root>(cacheKey) ?? new();
    }
    private async Task AddTitlesToStorage(string cacheKey)
    {
        await _storageService!.SaveToStorageAsync(cacheKey, Titles);
    }

    private (string message, MessageType type) GetUserFeedbackMessage()
    {
        if (string.IsNullOrEmpty(RegionToFilter) && string.IsNullOrEmpty(Recipient))
        {
            return ("Recipes loaded with no selection", MessageType.Info);  // Return both message and type
        }

        if (string.IsNullOrEmpty(RegionToFilter))
        {
            return ($"Recipes loaded for ingredient {Recipient}", MessageType.Info);
        }

        if (string.IsNullOrEmpty(Recipient))
        {
            return ($"Recipes loaded for region {RegionToFilter}", MessageType.Info);
        }

        return ($"Recipes loaded for region '{RegionToFilter}' and ingredient '{Recipient}'", MessageType.Info);
    }
    private async void ShowNetworkAlert(string message)
    {
        await ShowUserFeedbackAsync(message, MessageType.Error, Colors.Red, Colors.White, durationInSeconds: 5);
    }

    private async Task ShowUserFeedbackAsync(string message, MessageType messageType, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 5)
    {
        await _alertService.ShowInfoOrAlert(message, messageType, backgroundColor, textColor, durationInSeconds);
    }
}