using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class ViewSummaryPage : ContentPage
{
    public RecipeDetailViewModel _viewModel { get; }

    public ViewSummaryPage(RecipeDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}