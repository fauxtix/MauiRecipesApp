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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _spoonacularViewModel.LoadRecentSearchesOnAppearingCommand.Execute(this);
        _spoonacularViewModel.LoadRecipesDetailsCommand.Execute(this);
        _spoonacularViewModel.LoadPopularRecipesCommand.Execute(this);
    }

    private void Button10_Clicked(object sender, EventArgs e)
    {
        if (_spoonacularViewModel.NumberOfRecipes != 10)
        {
            _spoonacularViewModel.NumberOfRecipes = 10;
            _spoonacularViewModel.Is10Enabled = false;
            _spoonacularViewModel.Is20Enabled = true;
            _spoonacularViewModel.Is30Enabled = true;
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
        }
    }

    private async void OpenBottomSlider_Clicked(object sender, EventArgs e)
    {
        await _spoonacularViewModel.ShowOptionsFromBottomSheet(Window);
        //var nr = Preferences.Get("NumberOfRecipes", _spoonacularViewModel.NumberOfRecipes);
        //_spoonacularViewModel.NumberOfRecipes = nr;
    }
}