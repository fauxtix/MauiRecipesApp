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

    private void Button10_Clicked(object sender, EventArgs e)
    {
        if (_spoonacularViewModel.NumberOfRecipes != 10)
        {
            _spoonacularViewModel.NumberOfRecipes = 10;
            _spoonacularViewModel.Is10Enabled = false;
            _spoonacularViewModel.Is20Enabled = true;
            _spoonacularViewModel.Is30Enabled = true;
            //await _spoonacularViewModel.GetRecipesTitles();
        }
    }

    private void Button20_Clicked(object sender, EventArgs e)
    {
        if (_spoonacularViewModel.NumberOfRecipes != 20)
        {
            _spoonacularViewModel.NumberOfRecipes = 20;
            _spoonacularViewModel.Is10Enabled = true;
            _spoonacularViewModel.Is20Enabled = false;
            _spoonacularViewModel.Is30Enabled = true;
            //await _spoonacularViewModel.GetRecipesTitles();
        }
    }
    private void Button30_Clicked(object sender, EventArgs e)
    {
        if (_spoonacularViewModel.NumberOfRecipes != 30)
        {
            _spoonacularViewModel.NumberOfRecipes = 30;
            _spoonacularViewModel.Is10Enabled = true;
            _spoonacularViewModel.Is20Enabled = true;
            _spoonacularViewModel.Is30Enabled = false;
            //await _spoonacularViewModel.GetRecipesTitles();
        }
    }
}