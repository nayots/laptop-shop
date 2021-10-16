using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Common.Models.Config;

namespace Nayots.LaptopShop.Data.Shared
{
    public class SqliteConnectionProvider : IConnectionProvider
    {
        private readonly IOptionsMonitor<DbConfig> _dbConfig;
        private SqliteConnection _masterConnection;

        public SqliteConnectionProvider(IOptionsMonitor<DbConfig> dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public SqliteConnection GetConnection()
        {
            if (_masterConnection == null)
            {
                _masterConnection = new SqliteConnection(_dbConfig.CurrentValue.Name);
                _masterConnection.Open();
            }

            return _masterConnection;
        }
    }
}