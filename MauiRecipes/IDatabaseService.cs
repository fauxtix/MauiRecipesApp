using SQLite;

namespace MauiRecipes.Services.Interfaces
{
    public interface IDatabaseService
    {
        SQLiteConnection Connection { get; }
    }
}