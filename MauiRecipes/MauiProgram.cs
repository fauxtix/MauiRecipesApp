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
                    fonts.AddFont("OpenSans-Medium.ttf", "sans-serif-medium");
                    fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerifBold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "Material");
                });

            // Services
            builder.Services.AddTransient<IDapperContext, DapperContext>();
            builder.Services.AddTransient<ISpoonacularService, SpoonacularService>();

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
