using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class RecipesListPage : ContentPage
{
    private readonly RecipeListViewModel _viewModel;

    public RecipesListPage(RecipeListViewModel viewModell)
    {
        InitializeComponent();
        _viewModel = viewModell;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetRecipesTitlesCommand.Execute(null);
    }
}