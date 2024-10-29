using MauiRecipes.Services.Interfaces.DapperContext;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MauiRecipes.Contexts
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("RecipesConnection");
        }

        public void Execute(Action<IDbConnection> @event)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                @event(connection);
            }
        }
        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}
