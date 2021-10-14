using Dapper;
using Nayots.LaptopShop.Common.Contracts.Data;
using System.IO;

namespace Nayots.LaptopShop.Data.BootStrap
{
    public class DataBootstrap : IDataBoostrap
    {
        private readonly IConnectionProvider _connectionProvider;

        public DataBootstrap(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Setup()
        {
            var connection = _connectionProvider.GetConnection();

            var bootstrapSql = File.ReadAllText("BootstrapScript.sql");
            if (string.IsNullOrWhiteSpace(bootstrapSql))
                return;

            connection.Execute(bootstrapSql);
        }
    }
}