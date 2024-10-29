using MauiRecipes.MVVM.ViewModels;

namespace MauiRecipes.MVVM.Views;

public partial class RecipesMainPage : ContentPage
{
    private readonly SpoonacularViewModel _spoonacularViewModel;
    public RecipesMainPage(SpoonacularViewModel spoonacularViewModel)
    {
        InitializeComponent();
        _spoonacularViewModel = spoonacularViewModel;
        BindingContext = _spoonacularViewModel;
    }
}