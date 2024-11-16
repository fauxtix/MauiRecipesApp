using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class SavedSearchesPage : ContentPage
{
    private readonly SavedSearchesViewModel _viewModel;

    public SavedSearchesPage(SavedSearchesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}