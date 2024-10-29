using System.Data;

namespace MauiRecipes.Services.Interfaces.DapperContext
{
    public interface IDapperContext
    {
        public IDbConnection CreateConnection();
        public void Execute(Action<IDbConnection> @event);

    }
}
