using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;

namespace MauiRecipes.MVVM.ViewModels
{
    public partial class SavedSearchesViewModel : ObservableObject
    {
        private readonly IRecipeStorageService _storageService;
        private readonly IAlertService _alertService;


        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        private List<SavedSearches> savedSearchesList = new();

        public ObservableCollection<SavedSearches> SavedSearchesCollection { get; } = new();

        public SavedSearchesViewModel(IRecipeStorageService storageService, IAlertService alertService)
        {
            _storageService = storageService;
            _alertService = alertService;

            GetFirstData();
        }


        private async void GetFirstData()
        {
            await LoadSavedSearches();
        }


        [RelayCommand]
        public async Task LoadSavedSearches()
        {
            try
            {
                IsBusy = true;

                SavedSearchesList = await _storageService.GetSavedSearches();
                if (SavedSearchesList is not null)
                {
                    SavedSearchesCollection.Clear();
                    if (SavedSearchesList != null)
                    {
                        foreach (var search in SavedSearchesList)
                        {
                            SavedSearchesCollection.Add(search!);
                        }
                    }
                }

                await _alertService.ShowInfoOrAlert(
                    message: SavedSearchesList?.Any() == true ? "Saved searches loaded successfully." : "No searches found.",
                    type: MessageType.Info, null, null, 1);
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
    }
}
