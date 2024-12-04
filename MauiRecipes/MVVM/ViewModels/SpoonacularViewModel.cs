using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiRecipes.Messaging;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;
using static MauiRecipes.MVVM.Models.RecipeInformation;

namespace MauiRecipes.MVVM.ViewModels;
public partial class SpoonacularViewModel : BaseViewModel, IDisposable
{
    private readonly ISpoonacularService? _service;
    private readonly IRecipeStorageService? _storageService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    private CountriesCuisines.Root? titles;
    [ObservableProperty]
    private List<Recipes.MyArray>? recipeDetail;

    [ObservableProperty]
    private RecipeInfo? recipeInfo;

    [ObservableProperty]
    private string? ingredientFilter;

    public ObservableCollection<CountriesCuisines.Result?> RecipesTitles { get; } = new();
    public ObservableCollection<Recipes.MyArray?> RecipeDetails { get; } = new();

    public ObservableCollection<RecipeInfo> PopularRecipes { get; set; } = new();

    [ObservableProperty]
    private bool isInitialLoadComplete = false;

    // popular searches
    [ObservableProperty]
    private bool _showPopular = true;
    [ObservableProperty]
    private bool _viewRecipesFromSavedSearches = false;

    [ObservableProperty]
    private List<SavedSearches> savedSearchesList = new();

    public ObservableCollection<SavedSearches> SavedSearchesCollection { get; } = new();


    [ObservableProperty]
    private List<RecipeInformation.RecipeInfo> recipesDetailsList = new();

    public ObservableCollection<RecipeInformation.RecipeInfo> RecipesDetailsCollection { get; } = new();

    public SpoonacularViewModel(ISpoonacularService service,
        IRecipeStorageService storageService, IAlertService alertService)
    {
        _service = service;
        _storageService = storageService;
        _alertService = alertService;

        PropertyChanged += SpoonacularViewModel_PropertyChanged!;

        InitializeRegions();

        LoadInitialSearches();
        LoadRecipesDetails();
        LoadInitialPopularRecipes();
    }

    private async void LoadInitialSearches()
    {
        await LoadRecentSearchesOnAppearing();
    }

    private async void LoadRecipesDetails()
    {
        await LoadRecipesDetailsAsync();
    }
    private async void LoadInitialPopularRecipes()
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
        {
            ShowNetworkAlert("No internet access");
            return;
        }

        await LoadPopularRecipes();
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
    private async Task SearchRecipes()
    {
        int numberOfRecipes;
        if (!ViewRecipesFromSavedSearches)
        {
            numberOfRecipes = Preferences.Get("NumberOfRecipes", NumberOfRecipes);
        }
        else
        {
            numberOfRecipes = NumberOfRecipes;
        }

        var recipesList = await _service!.GetRecipeTitles(RegionToFilter, IngredientFilter!, numberOfRecipes);
        if (recipesList.results.Count == 0)
        {
            await ShowUserFeedbackAsync("No results found...", MessageType.Warning, backgroundColor: Colors.Orange,
                textColor: Colors.Black, durationInSeconds: 5);
            return;
        }

        ViewRecipesFromSavedSearches = false;

        SavedSearches savedSearches = new()
        {
            Region = RegionToFilter,
            Ingredient = IngredientFilter,
            NumberOfRecipes = numberOfRecipes
        };

        await Shell.Current.GoToAsync($"{nameof(RecipesListPage)}", true, new Dictionary<string, object>
            {
                {"SavedSearches", savedSearches},
             });
    }

    [RelayCommand]
    public void ResetSearch()
    {
        SelectedRegion = null;
        RegionToFilter = "";
        IngredientFilter = "";
    }

    [RelayCommand]
    public async Task DeleteSearch(SavedSearches savedSearch)
    {
        try
        {
            int searchId = savedSearch.Id;
            bool okToDelete = await Shell.Current.DisplayAlert("Please confirm", $"Delete search?", "Yes", "No");
            if (okToDelete)
            {
                IsBusy = true;
                await Task.Yield();

                await _storageService!.DeleteSearchAsync(searchId);

                await LoadRecentSearchesOnAppearing();

                await ShowUserFeedbackAsync("Search removed fromthe  database.", MessageType.Warning, durationInSeconds: 2);
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

                IngredientFilter = "";
                SelectedRegion = new();

                await ShowUserFeedbackAsync("All cached searches removed from database.", MessageType.Warning, durationInSeconds: 5);

                await LoadRecentSearchesOnAppearing();
                await LoadRecipesDetailsAsync();
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

    [RelayCommand]
    public async Task LoadRecentSearchesOnAppearing()
    {
        IsBusy = true;
        await Task.Yield();
        try
        {
            SavedSearchesList = await _storageService.GetSavedSearches();
            SavedSearchesCollection.Clear();
            if (SavedSearchesList != null)
            {
                foreach (var search in SavedSearchesList)
                {
                    SavedSearchesCollection.Add(search!);
                }
            }

        }
        finally
        {
            IsBusy = false;
        }
    }

    // Recent searches

    [RelayCommand]
    public async Task LoadSavedSearches()
    {
        try
        {
            IsBusy = true;
            await Task.Yield();

            WeakReferenceMessenger.Default.Unregister<SavedSearchesUpdatedMessage>(this);

            SavedSearchesList = await _storageService.GetSavedSearches();
            WeakReferenceMessenger.Default.Register<SavedSearchesUpdatedMessage>(this, (r, m) =>
            {
                SavedSearchesCollection.Clear();
                if (SavedSearchesList != null)
                {
                    foreach (var search in SavedSearchesList)
                    {
                        SavedSearchesCollection.Add(search!);
                    }
                }
            });

        }
        catch (Exception ex)
        {
            await _alertService.ShowInfoOrAlert($"Failed to load recent searches: {ex.Message}", MessageType.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task LoadRecipesDetailsAsync()
    {
        try
        {
            IsBusy = true;
            await Task.Yield();

            RecipesDetailsList = (await _storageService!.GetRecipesDetailsStored<RecipeInformation.RecipeInfo>()).ToList();
            RecipesDetailsCollection.Clear();
            if (RecipesDetailsList != null)
            {
                foreach (var recipeDetail in RecipesDetailsList)
                {
                    RecipesDetailsCollection.Add(recipeDetail!);
                }
            }
        }
        catch (Exception ex)
        {
            await _alertService.ShowInfoOrAlert($"Failed to load recipes details: {ex.Message}", MessageType.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public void SetParameters(SavedSearches savedSearch)
    {
        if (savedSearch!.Ingredient!.ToLower().Equals("no ingredient"))
        {
            IngredientFilter = "";
        }
        else
        {
            IngredientFilter = savedSearch.Ingredient;
        }

        if (savedSearch!.Region!.ToLower().Equals("no region"))
        {
            RegionToFilter = "";
        }
        else
        {
            if (!string.IsNullOrEmpty(savedSearch.Region))
            {
                var selRegion = Regions.FirstOrDefault(r => r.ID == savedSearch.Region);

                if (selRegion != null)
                {
                    SelectedRegion = selRegion;
                }
                else
                {
                    SelectedRegion = null;
                }
            }

        }
        ViewRecipesFromSavedSearches = true;
        NumberOfRecipes = savedSearch.NumberOfRecipes;

    }

    [RelayCommand]
    public async Task LoadPopularRecipes()
    {
        var recipes = await _service!.GetPopularRecipesAsync(RegionToFilter, IngredientFilter!, NumberOfRecipes);
        PopularRecipes.Clear();
        foreach (var recipe in recipes.Results)
        {
            PopularRecipes.Add(recipe);
        }
    }

    [RelayCommand]
    private async Task ViewPopularRecipeDetail(RecipeInfo recipeInfo)
    {
        IsBusy = true;
        await Task.Yield();

        try
        {
            var response = await _service!.GetRecipeInformation(recipeInfo.id);
            await Shell.Current.GoToAsync($"{nameof(ViewRecipePage)}", true, new Dictionary<string, object>
            {
                {"RecipeInfo", response},
                { "IsFavorite", false },
                { "ShowFavoriteIcon", false }
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
    private async Task ViewSavedRecipeDetail(RecipeInfo recipeInfo)
    {
        IsBusy = true;
        await Task.Yield();

        try
        {
            var response = await _storageService!.LoadDetailFromStorageAsync<RecipeInfo>(recipeInfo.id);
            await Shell.Current.GoToAsync($"{nameof(ViewRecipePage)}", true, new Dictionary<string, object>
            {
                {"RecipeInfo", response.Recipe!},
                { "IsFavorite", response.IsFavorite },
                { "ShowFavoriteIcon", true }
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
    public async Task ShowOptionsFromBottomSheet(Window window)
    {
        var page = new SettingsPage();
        await page.ShowAsync(window, true);
    }


    //public void EnableDisableNumberOfRecipesButtons(int numberOfRecipesButtons)
    //{
    //    if (numberOfRecipesButtons == 10)
    //    {
    //        Is10Enabled = false;
    //        Is20Enabled = true;
    //        Is30Enabled = true;
    //    }
    //    else if (numberOfRecipesButtons == 20)
    //    {
    //        Is10Enabled = true;
    //        Is20Enabled = false;
    //        Is30Enabled = true;
    //    }
    //    else if (numberOfRecipesButtons == 30)
    //    {
    //        Is10Enabled = true;
    //        Is20Enabled = true;
    //        Is30Enabled = false;
    //    }

    //}

    private void SpoonacularViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedRegion) && SelectedRegion != null)
        {
            RegionToFilter = SelectedRegion.ID;
        }
    }

    private (string message, MessageType type) GetUserFeedbackMessage()
    {
        if (string.IsNullOrEmpty(RegionToFilter) && string.IsNullOrEmpty(IngredientFilter))
        {
            return ("Random recipes loaded (no selection) from the Api", MessageType.Info);  // Return both message and type
        }

        if (string.IsNullOrEmpty(IngredientFilter))
        {
            return ($"Recipes loaded for ingredient {IngredientFilter} from the Api", MessageType.Info);
        }

        if (string.IsNullOrEmpty(RegionToFilter))
        {
            return ($"Recipes loaded for region {RegionToFilter} from the Api", MessageType.Info);
        }

        return ($"Recipes loaded for region '{RegionToFilter}' and ingredient '{IngredientFilter} from the Api'", MessageType.Info);
    }
    private async void ShowNetworkAlert(string message)
    {
        await ShowUserFeedbackAsync(message, MessageType.Error, Colors.Red, Colors.White, durationInSeconds: 5);
    }

    private async Task ShowUserFeedbackAsync(string message, MessageType messageType, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 2)
    {
        await _alertService.ShowInfoOrAlert(message, messageType, backgroundColor, textColor, durationInSeconds);
    }

    public void Dispose()
    {
        PropertyChanged -= SpoonacularViewModel_PropertyChanged!;
        WeakReferenceMessenger.Default.Unregister<SavedSearchesUpdatedMessage>(this);
    }
    ~SpoonacularViewModel()
    {
        Dispose();
    }
}