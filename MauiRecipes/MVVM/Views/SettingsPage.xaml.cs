using System.Collections.ObjectModel;
using System.Windows.Input;
using The49.Maui.BottomSheet;

namespace MauiRecipes.MVVM.Views;

public class ListAction
{
    public string? Title { get; set; }
    public ICommand? Command { get; set; }
}

public partial class SettingsPage : BottomSheet
{
    public int NumberOfRecipes { get; set; }


    public SettingsPage()
    {
        InitializeComponent();
        LoadNumberOfRecipes();
        SetButtonHighlight(NumberOfRecipes);
    }

    public ObservableCollection<ListAction> Actions => new()
    {
        new ListAction
        {
            Title = "Save",
            Command = new Command(() => SaveSettingsAsync()),
        },
        new ListAction
        {
            Title = "Dismiss",
            Command = new Command(() => DismissAsync()),
        }
    };

    public void LoadNumberOfRecipes()
    {
        NumberOfRecipes = Preferences.Get("NumberOfRecipes", 10);
    }
    void SaveSettingsAsync()
    {
        Preferences.Set("NumberOfRecipes", NumberOfRecipes);
        DismissAsync();
    }

    void Resize()
    {
        divider.HeightRequest = 32;
    }

    public VisualElement Divider => divider;

    public void SetExtraContent(View view)
    {
        extra.Content = view;
    }

    private void SetButtonHighlight(int numberOfRecipes)
    {
        var defaultColor = Colors.SteelBlue;

        Rec_10.BorderColor = defaultColor;
        Rec_20.BorderColor = defaultColor;
        Rec_30.BorderColor = defaultColor;

        var highlightColor = Colors.Red;

        switch (numberOfRecipes)
        {
            case 10:
                Rec_10.BorderColor = highlightColor;
                break;
            case 20:
                Rec_20.BorderColor = highlightColor;
                break;
            case 30:
                Rec_30.BorderColor = highlightColor;
                break;
            default:
                // Optional: Handle unexpected values
                break;
        }
    }
    private void Button10_Clicked(object sender, EventArgs e)
    {
        NumberOfRecipes = 10;
        Rec_10.IsEnabled = false;
        Rec_20.IsEnabled = true;
        Rec_30.IsEnabled = true;
        Rec_10.BorderColor = Colors.Red;
        Rec_20.BorderColor = Colors.SteelBlue;
        Rec_30.BorderColor = Colors.SteelBlue;

    }

    private void Button20_Clicked(object sender, EventArgs e)
    {
        NumberOfRecipes = 20;
        Rec_10.IsEnabled = true;
        Rec_20.IsEnabled = false;
        Rec_30.IsEnabled = true;
        Rec_10.BorderColor = Colors.SteelBlue;
        Rec_20.BorderColor = Colors.Red;
        Rec_30.BorderColor = Colors.SteelBlue;

    }
    private void Button30_Clicked(object sender, EventArgs e)
    {
        NumberOfRecipes = 30;
        Rec_10.IsEnabled = true;
        Rec_20.IsEnabled = true;
        Rec_30.IsEnabled = false;
        Rec_10.BorderColor = Colors.SteelBlue;
        Rec_20.BorderColor = Colors.SteelBlue;
        Rec_30.BorderColor = Colors.Red;

    }

}
