using CommunityToolkit.Maui;
using MauiRecipes.MVVM.ViewModels;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Implementations;
using MauiRecipes.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace MauiRecipes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-Medium.ttf", "sans-serif-medium");
                    fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerifBold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                });


            // Services
            builder.Services.AddTransient<ISpoonacularService, SpoonacularService>();
            builder.Services.AddTransient<IRecipeStorageService, RecipeStorageService>();
            builder.Services.AddTransient<IAlertService, AlertService>();


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db3");

            builder.Services.AddSingleton<IRecipeStorageService>(serviceProvider =>
                new RecipeStorageService(databasePath));

            // ViewModels
            builder.Services.AddSingleton<BaseViewModel>();
            builder.Services.AddTransient<SpoonacularViewModel>();
            builder.Services.AddTransient<RecipeDetailViewModel>();


            // Views
            builder.Services.AddTransient<RecipesMainPage>();
            builder.Services.AddTransient<ViewRecipePage>();
            builder.Services.AddTransient<ViewSummaryPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
