using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Common.Models.Config;
using System.IO;

namespace Nayots.LaptopShop.Data.BootStrap
{
    public class DataBootstrap : IDataBoostrap
    {
        private readonly IOptionsMonitor<DbConfig> _dbOptions;

        public DataBootstrap(IOptionsMonitor<DbConfig> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_dbOptions.CurrentValue.Name);

            var bootstrapSql = File.ReadAllText("BootstrapScript.sql");
            if (string.IsNullOrWhiteSpace(bootstrapSql))
                return;

            connection.Execute(bootstrapSql);
        }
    }
}
