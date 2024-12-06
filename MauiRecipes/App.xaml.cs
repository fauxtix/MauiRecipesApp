namespace MauiRecipes
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }
        public App(IServiceProvider services)
        {
            InitializeComponent();
            MainPage = new AppShell();
            Services = services;
        }
    }
}
