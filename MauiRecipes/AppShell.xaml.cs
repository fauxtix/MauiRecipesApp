using MauiRecipes.MVVM.Views;

namespace MauiRecipes
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;

        }

        void RegisterRoutes()
        {
            Routes.Add(nameof(RecipesMainPage), typeof(RecipesMainPage));
            Routes.Add(nameof(ViewRecipePage), typeof(ViewRecipePage));
            Routes.Add(nameof(FavoritesPage), typeof(FavoritesPage));
            Routes.Add(nameof(ViewFavoriteRecipeDetailPage), typeof(ViewFavoriteRecipeDetailPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }

        }
    }
}
