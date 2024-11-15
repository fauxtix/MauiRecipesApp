using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewmodel;

    public FavoritesPage(FavoritesViewModel viewmodel)
    {
        InitializeComponent();
        _viewmodel = viewmodel;
        BindingContext = _viewmodel;
    }
}