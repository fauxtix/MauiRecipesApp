using CommunityToolkit.Maui;
using MauiRecipes.Contexts;
using MauiRecipes.MVVM.ViewModels;
using MauiRecipes.MVVM.Views;
using MauiRecipes.Services.Implementations;
using MauiRecipes.Services.Interfaces;
using MauiRecipes.Services.Interfaces.DapperContext;
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
                });

            // Services
            builder.Services.AddTransient<IDapperContext, DapperContext>();
            builder.Services.AddTransient<ISpoonacularService, SpoonacularService>();

            // ViewModels
            builder.Services.AddSingleton<BaseViewModel>();
            builder.Services.AddTransient<SpoonacularViewModel>();


            // Views
            builder.Services.AddTransient<RecipesMainPage>();
            builder.Services.AddTransient<ViewRecipePage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
