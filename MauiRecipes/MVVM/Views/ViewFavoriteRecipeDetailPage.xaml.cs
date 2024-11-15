using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class ViewFavoriteRecipeDetailPage : ContentPage
{
    private readonly FavoriteRecipeDetailViewModel _favoriteRecipeDetailViewModel;

    public ViewFavoriteRecipeDetailPage(FavoriteRecipeDetailViewModel favoriteRecipeDetailViewModel)
    {
        InitializeComponent();
        _favoriteRecipeDetailViewModel = favoriteRecipeDetailViewModel;
        BindingContext = _favoriteRecipeDetailViewModel;

    }
}