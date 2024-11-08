using MauiRecipes.MVVM.Models;
using MauiRecipes.Services.Interfaces;
using SQLite;

namespace MauiRecipes.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteConnection _connection;

        public DatabaseService()
        {
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");

            _connection = new SQLiteConnection(databasePath);

            _connection.CreateTable<LocalRecipeData>();
        }

        public SQLiteConnection Connection => _connection;
    }
}
