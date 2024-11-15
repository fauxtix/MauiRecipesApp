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
                // In case no data returned from the database, use the Api; if there's a valid response, store the results in the database
                // (in case a Region was selected)

                var (quotaUsed, quotaLeft, requestCost) = await _service!.GetQuotaDetailsAsync("recipes/complexSearch");

                if (quotaUsed == -1)
                {
                    await ShowUserFeedbackAsync(message: "Error getting Api quota usage.",
                      messageType: MessageType.Error, null, textColor: Colors.White, durationInSeconds: 5);
                    return;

                }

                ApiQuotaUsed = quotaUsed;
                ApiQuotaLeft = quotaLeft;
                ApiRequestCost = requestCost;

                if (TotalQuota > 0)
                {
                    RequestsProgress = ApiQuotaUsed / TotalQuota;
                }
                else
                {
                    RequestsProgress = 0;
                }

                if (quotaLeft <= 0)
                {
                    await ShowUserFeedbackAsync("You have no more quota left for today.",
                        MessageType.Error, durationInSeconds: 5);
                    return;
                }

                Titles = await _service!.GetRecipeTitles(RegionToFilter, Recipient!, NumberOfRecipes);
                if (Titles.results.Count > 0)
                {
                    AddRecipesToTitlesCollection();
                    if (!string.IsNullOrEmpty(RegionToFilter))
                    {
                        await AddTitlesToStorage(cacheKey);
                    }

                    var userFeedback = GetUserFeedbackMessage();
                    await ShowUserFeedbackAsync(userFeedback.message, userFeedback.type, durationInSeconds: 2);
                }
                else
                {
                    await ShowUserFeedbackAsync("No results found...", MessageType.Warning, null, Colors.Black, durationInSeconds: 5);
                }
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
        //if (string.IsNullOrEmpty(Recipient) && string.IsNullOrEmpty(RegionToFilter))
        //    return;

        await GetRecipesTitles();
    }


    [RelayCommand]
    private async Task GetRecipeInformation(CountriesCuisines.Result param)
    {
        IsBusy = true;
        await Task.Yield();

        try
        {
            var recipeInfo = await FetchRecipeInformationFromStorageOrApi(param.Id);

            if (recipeInfo.Recipe == null)
            {
                await ShowUserFeedbackAsync("Failed to load recipe information.", MessageType.Error);
                return;
            }

            if (!recipeInfo.FromDatabase)
            {
                var endpoint = $"recipes/{param.Id}/information";
                var (quotaUsed, quotaLeft, requestCost) = await _service!.GetQuotaDetailsAsync(endpoint);

                if (quotaUsed == -1)
                {
                    await ShowUserFeedbackAsync(message: "Error getting Api quota usage.",
                      messageType: MessageType.Error, null, textColor: Colors.White, durationInSeconds: 5);
                    return;
                }

                ApiQuotaUsed = quotaUsed;
                ApiQuotaLeft = quotaLeft;
                ApiRequestCost = requestCost;

                if (TotalQuota > 0)
                {
                    RequestsProgress = ApiQuotaUsed / TotalQuota;
                }
                else
                {
                    RequestsProgress = 0;
                }

                if (quotaLeft <= 0)
                {
                    await ShowUserFeedbackAsync("You have no more quota left for today.",
                        MessageType.Error, durationInSeconds: 5);
                    return;
                }
            }

            await Shell.Current.GoToAsync($"//{nameof(ViewRecipePage)}", true, new Dictionary<string, object>
            {
                {"RecipeInfo", recipeInfo.Recipe},
                { "IsFavorite", recipeInfo.IsFavorite }
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
                await ShowUserFeedbackAsync("All cached searches removed from database.", MessageType.Info, durationInSeconds: 5);

                await GetRecipesTitles();
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


    private async Task<(RecipeInformation.RecipeInfo Recipe, bool IsFavorite, bool FromDatabase)> FetchRecipeInformationFromStorageOrApi(int recipeId)
    {
        var (recipeFromStorage, isFavorite) = await _storageService!.LoadDetailFromStorageAsync<RecipeInformation.RecipeInfo>(recipeId);
        bool fromDatabase;
        RecipeInformation.RecipeInfo recipeInfo;

        if (recipeFromStorage == null)
        {
            fromDatabase = false;

            recipeInfo = await _service!.GetRecipeInformation(recipeId);

            await _storageService.SaveDetailToStorageAsync(recipeId, recipeInfo);
            isFavorite = false;
            await ShowUserFeedbackAsync("Recipe detail loaded from the API.", MessageType.Info, durationInSeconds: 2);
        }
        else
        {
            fromDatabase = true;
            recipeInfo = recipeFromStorage;
            await ShowUserFeedbackAsync("Recipe detail loaded from database.", MessageType.Success, durationInSeconds: 2);
        }

        return (recipeInfo, isFavorite, fromDatabase);
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
            return ("Random recipes loaded (no selection) from the Api", MessageType.Info);  // Return both message and type
        }

        if (string.IsNullOrEmpty(Recipient))
        {
            return ($"Recipes loaded for ingredient {Recipient} from the Api", MessageType.Info);
        }

        if (string.IsNullOrEmpty(RegionToFilter))
        {
            return ($"Recipes loaded for region {RegionToFilter} from the Api", MessageType.Info);
        }

        return ($"Recipes loaded for region '{RegionToFilter}' and ingredient '{Recipient} from the Api'", MessageType.Info);
    }
    private async void ShowNetworkAlert(string message)
    {
        await ShowUserFeedbackAsync(message, MessageType.Error, Colors.Red, Colors.White, durationInSeconds: 5);
    }

    private async Task ShowUserFeedbackAsync(string message, MessageType messageType, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 5)
    {
        await _alertService.ShowInfoOrAlert(message, messageType, backgroundColor, textColor, durationInSeconds);
    }

    //~SpoonacularViewModel()
    //{
    //    PropertyChanged -= SpoonacularViewModel_PropertyChanged!;
    //}
}