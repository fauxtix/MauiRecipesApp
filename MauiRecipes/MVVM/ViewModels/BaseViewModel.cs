using CommunityToolkit.Mvvm.ComponentModel;
using MauiRecipes.MVVM.Models;
using System.Collections.ObjectModel;

namespace MauiRecipes.MVVM.ViewModels;
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    private bool isEditing;
    [ObservableProperty]
    public string? _editCaption;

    [ObservableProperty]
    private string? image;
    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string _regionToFilter = "American";

    [ObservableProperty]
    private int _numberOfRecipes = 10;
    [ObservableProperty]
    private bool _is10Enabled = false;
    [ObservableProperty]
    private bool _is20Enabled = true;
    [ObservableProperty]
    private bool _is30Enabled = true;

    [ObservableProperty]
    private CuisineRegion? selectedRegion;

    [ObservableProperty]
    public List<CuisineRegion> regionsData = new();

    public ObservableCollection<CuisineRegion> Regions { get; } = new();
}
