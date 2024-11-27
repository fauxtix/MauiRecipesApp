using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiRecipes.Messaging;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;

namespace MauiRecipes.MVVM.ViewModels
{
    [QueryProperty(nameof(SavedSearches), nameof(SavedSearches))]

    public partial class RecipeListViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ISpoonacularService? _service;
        private readonly IRecipeStorageService? _storageService;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private CountriesCuisines.Root? titles;
        public ObservableCollection<CountriesCuisines.Result?> RecipesTitles { get; } = new();

        [ObservableProperty]
        private string? ingredientFilter;

        [ObservableProperty]
        private string? regionCaption;
        [ObservableProperty]
        private string? ingredientCaption;

        [ObservableProperty]
        public bool areThereParameters;
        public RecipeListViewModel(ISpoonacularService service,
        IRecipeStorageService storageService, IAlertService alertService)
        {
            _service = service;
            _storageService = storageService;
            _alertService = alertService;

        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var data = query[nameof(SavedSearches)] as SavedSearches;
            IngredientFilter = data?.Ingredient;
            RegionToFilter = data?.Region ?? "";
            IngredientCaption = !string.IsNullOrEmpty(data?.Ingredient) ? data.Ingredient : "No ingredient";
            RegionCaption = !string.IsNullOrEmpty(data?.Region) ? data.Region : "No region";
            NumberOfRecipes = data?.NumberOfRecipes ?? 10;

            GetRecipes();
        }

        private async void GetRecipes()
        {
            await GetRecipesTitles();
        }

        [RelayCommand]
        public async Task GetRecipesTitles()
        {

            IsBusy = true;
            await Task.Yield();

            try
            {
                //if (Recipient!.ToLower().Equals("no ingredient"))
                //{
                //    Recipient = "";
                //}

                string region = !string.IsNullOrEmpty(RegionToFilter) ? RegionToFilter : "defaultRegion";
                string recipient = !string.IsNullOrEmpty(IngredientFilter) ? IngredientFilter : "defaultIngredient";
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

                    //if (quotaLeft <= 0)
                    //{
                    //    await ShowUserFeedbackAsync("You have no more quota left for today.",
                    //        MessageType.Error, durationInSeconds: 10);
                    //    return;
                    //}

                    Titles = await _service!.GetRecipeTitles(RegionToFilter, IngredientFilter!, NumberOfRecipes);
                    if (Titles.results.Count > 0)
                    {
                        AddRecipesToTitlesCollection();
                        if (!string.IsNullOrEmpty(RegionToFilter))
                        {
                            await AddTitlesToStorage(cacheKey);
                        }

                        var userFeedback = GetUserFeedbackMessage();
                        await ShowUserFeedbackAsync(userFeedback.message, userFeedback.type, durationInSeconds: 1);
                    }
                    else
                    {
                        await ShowUserFeedbackAsync("No results found...", MessageType.Warning, backgroundColor: Colors.Orange,
                            textColor: Colors.Black, durationInSeconds: 5);
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

                    //if (quotaLeft <= 0)
                    //{
                    //    await ShowUserFeedbackAsync("You have no more quota left for today.",
                    //        MessageType.Error, durationInSeconds: 10);
                    //    return;
                    //}
                }

                await Shell.Current.GoToAsync($"{nameof(ViewRecipePage)}", true, new Dictionary<string, object>
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
        public async Task GoBack()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("..");
            //await Shell.Current.GoToAsync($"{nameof(RecipesMainPage)}");

            IsBusy = false;
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
                await ShowUserFeedbackAsync("Recipe detail loaded from the API.", MessageType.Info);
            }
            else
            {
                fromDatabase = true;
                recipeInfo = recipeFromStorage;
                await ShowUserFeedbackAsync("Recipe detail loaded from database.", MessageType.Success);
            }

            return (recipeInfo, isFavorite, fromDatabase);
        }

        private void AddRecipesToTitlesCollection()
        {
            RecipesTitles.Clear();
            foreach (var recipe in Titles!.results)
            {
                RecipesTitles.Add(recipe);
            }
        }

        private async Task AddTitlesToStorage(string cacheKey)
        {
            await _storageService!.SaveToStorageAsync(cacheKey, Titles);
            await _storageService!.SaveSearch(RegionToFilter, IngredientFilter ?? "", NumberOfRecipes);

            // Crie a mensagem que será enviada (contempla a lista atualizada de pesquisas)
            var updatedSearches = await _storageService.GetSavedSearches();

            WeakReferenceMessenger.Default.Send(new SavedSearchesUpdatedMessage(updatedSearches));
        }


        private async Task<CountriesCuisines.Root> LoadTitlesFromCacheAsync(string cacheKey)
        {
            return await _storageService!.LoadFromStorageAsync<CountriesCuisines.Root>(cacheKey) ?? new();
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
    }
}
