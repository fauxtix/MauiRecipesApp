using CommunityToolkit.Mvvm.ComponentModel;
using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using System.Collections.ObjectModel;
namespace MauiRecipes.MVVM.ViewModels;
public partial class SpoonacularViewModel : BaseViewModel
{
    private readonly ISpoonacularService _service;

    [ObservableProperty]
    private CountriesCuisines.Root titles;

    public ObservableCollection<CountriesCuisines.Result> RecipesTitles { get; } = new();

    public readonly HttpClient Client = new HttpClient();

    public SpoonacularViewModel(ISpoonacularService service)
    {
        _service = service;
        GetRecipesTitles();
        regionsData =
[
    new() { ID= "German", RegionName = "Alemã" },
            new() { ID= "American", RegionName= "Americana" },
            new() { ID= "Latin American", RegionName = "América Latina" },
            new() { ID= "British", RegionName= "Britânica" },
            new() { ID= "Cajun", RegionName= "Cajun" },
            new() { ID= "Caribbean", RegionName= "Caraíbas" },
            new() { ID= "Korean", RegionName = "Coreana" },
            new() { ID= "Spanish", RegionName = "Espanhola"},
            new() { ID= "Eastern European", RegionName= "Europa de Leste" },
            new() { ID= "French", RegionName = "Francesa" },
            new() { ID= "Greek", RegionName = "Grega"},
            new() { ID= "Indian", RegionName = "Indiana" },
            new() { ID= "Irish", RegionName = "Irlandesa"},
            new() { ID= "Italian", RegionName = "Italiana" },
            new() { ID= "Japanese", RegionName = "Japonesa" },
            new() { ID= "Jewish", RegionName = "Judeia" },
            new() { ID= "Mediterranean", RegionName = "Mediterrânica" },
            new() { ID= "Mexican", RegionName = "Mexicana" },
            new() { ID= "Middle Eastern", RegionName = "Médio Oriente"},
            new() { ID= "Nordic", RegionName = "Nórdica" },
            new() { ID= "Southern", RegionName = "Sulista"},
            new() { ID= "Thai", RegionName = "Thai"},
            new() { ID= "Vietnamese", RegionName= "Vietnamita"}
];

        Regions.Clear();
        foreach (var region in regionsData)
        {
            Regions.Add(region);
        }
    }

    public async void GetRecipesTitles()
    {
        titles = await _service.GetRecipeTitles("French");
        RecipesTitles.Clear();
        foreach (var recipe in titles.results)
        {
            RecipesTitles.Add(recipe);
        }
    }

    //private async Task<CountriesCuisines.Root> GetRecipesTitles(string regionFilter)
    //{
    //    var output = await _service.GetRecipeTitles(regionFilter);
    //    return output;
    //}


    //[RelayCommand]
    //private async Task GetRecipeTitles()
    //{
    //    RecipesTitles = await _service.GetRecipeTitles(RegionName);
    //}
    //[RelayCommand]
    //private async Task GetRecipeDetails(int Id)
    //{
    //    RecipeDetails = await _service.GetRecipeDetails(Id);
    //}

    //[RelayCommand]
    //private async Task GetRecipeInformation(int Id)
    //{
    //    RecipeInformation = await _service.GetRecipeInformation(Id);
    //}

}
