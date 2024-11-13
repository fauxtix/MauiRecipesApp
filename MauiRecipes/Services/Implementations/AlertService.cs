using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiRecipes.Services.Interfaces;
using static MauiRecipes.MVVM.Models.Enums.UserMessages;
using Font = Microsoft.Maui.Font;

public class AlertService : IAlertService
{
    public async Task ShowInfoOrAlert(string message, MessageType type, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 5)
    {
        // Define default colors based on MessageType, if no custom colors are provided
        backgroundColor ??= type switch
        {
            MessageType.Info => Colors.Blue,
            MessageType.Success => Colors.Green,
            MessageType.Warning => Colors.Orange,
            MessageType.Error => Colors.Red,
            _ => Colors.Gray
        };

        textColor ??= Colors.White;

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = backgroundColor,
            TextColor = textColor,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(12),
            ActionButtonFont = Font.SystemFontOfSize(12),
            CharacterSpacing = 0.2
        };
        var snackbar = Snackbar.Make(message, null, "Ok", TimeSpan.FromSeconds(durationInSeconds), snackbarOptions);
        await snackbar.Show();
    }
}
