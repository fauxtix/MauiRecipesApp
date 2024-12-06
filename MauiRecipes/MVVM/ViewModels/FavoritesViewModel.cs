using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;

namespace MauiRecipes.MVVM.ViewModels;

public partial class FavoritesViewModel : ObservableObject
{
    private readonly IRecipeStorageService _storageService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    bool isBusy;
    [ObservableProperty]
    bool isRefreshing;


    [ObservableProperty]
    private List<FavoritesData?> favoritesList = new();


    [ObservableProperty]
    private FavoritesData? selectedFavoriteRecipe;

    public ObservableCollection<FavoritesData> FavoriteRecipes { get; } = new();

    public FavoritesViewModel(IRecipeStorageService storageService, IAlertService alertService)
    {
        _storageService = storageService;
        _alertService = alertService;

        GetFirstData();

    }

    private async void GetFirstData()
    {
        await LoadFavoriteRecipesAsync();
    }


    [RelayCommand]
    public async Task LoadFavoriteRecipesAsync()
    {
        try
        {
            IsBusy = true;
            await Task.Yield();

            FavoritesList = await _storageService.GetAllFavoritesAsync<FavoritesData?>();

            FavoriteRecipes.Clear();
            if (FavoritesList != null)
            {
                foreach (var favorite in FavoritesList)
                {
                    FavoriteRecipes.Add(favorite!);
                }
            }

            if (FavoritesList?.Any() == false)
            {
                await _alertService.ShowInfoOrAlert(
                    message: "No favorite recipes found.",
                    type: MessageType.Warning, null, null, 1);
            }

            //await _alertService.ShowInfoOrAlert(
            //    message: FavoritesList?.Any() == true ? "Favorite recipes loaded successfully." : "No favorite recipes found.",
            //    type: FavoritesList?.Any() == true ? MessageType.Success : MessageType.Warning, null, null, 1);
        }
        catch (Exception ex)
        {
            await _alertService.ShowInfoOrAlert($"Failed to load favorite recipes: {ex.Message}", MessageType.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectFavoriteRecipeAsync(FavoritesData selectedRecipe)
    {
        if (selectedRecipe == null)
        {
            await ShowUserFeedbackAsync("No recipe selected.", MessageType.Warning);
            return;
        }

        try
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ViewFavoriteRecipeDetailPage)}", true, new Dictionary<string, object>
            {
                { "FavoritesData", selectedRecipe }
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
        IsBusy = false;
    }


    private async Task ShowUserFeedbackAsync(string message, MessageType messageType, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 5)
    {
        await _alertService.ShowInfoOrAlert(message, messageType, backgroundColor, textColor, durationInSeconds);
    }
}
