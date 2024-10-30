using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class ViewRecipePage : ContentPage
{
    public RecipeDetailViewModel _viewModel { get; }

    public ViewRecipePage(RecipeDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}