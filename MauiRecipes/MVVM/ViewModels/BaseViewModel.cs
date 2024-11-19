﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    private string _regionToFilter = "";

    [ObservableProperty]
    private int _numberOfRecipes = 10;
    [ObservableProperty]
    private bool _is10Enabled = true;
    [ObservableProperty]
    private bool _is20Enabled = false;
    [ObservableProperty]
    private bool _is30Enabled = false;

    [ObservableProperty]
    private double apiQuotaUsed;
    [ObservableProperty]
    private double apiQuotaLeft;
    [ObservableProperty]
    private double apiRequestCost;


    //[ObservableProperty]
    //private double apiQuotaUsedDetails;
    //[ObservableProperty]
    //private double apiQuotaLeftDetails;
    //[ObservableProperty]
    //private double apiRequestCostDetails;

    [ObservableProperty]
    private double _requestsProgress = 0;
    //[ObservableProperty]
    //private double _requestsProgressDetails = 0;

    public double TotalQuota => ApiQuotaUsed + ApiQuotaLeft;
    //public double TotalQuotaDetails => ApiQuotaUsedDetails + ApiQuotaLeftDetails;


    [ObservableProperty]
    private CuisineRegion? selectedRegion;

    [ObservableProperty]
    public List<CuisineRegion> regionsData = new();

    public ObservableCollection<CuisineRegion> Regions { get; } = new();

}
