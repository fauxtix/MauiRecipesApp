using static MauiRecipes.MVVM.Models.Enums.UserMessages;

namespace MauiRecipes.Services.Interfaces
{
    public interface IAlertService
    {
        Task ShowInfoOrAlert(string message, MessageType type, Color? backgroundColor = null, Color? textColor = null, int durationInSeconds = 5);
    }
}