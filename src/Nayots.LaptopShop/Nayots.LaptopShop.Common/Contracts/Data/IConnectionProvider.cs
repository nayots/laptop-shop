using Microsoft.Data.Sqlite;

namespace Nayots.LaptopShop.Common.Contracts.Data
{
    public interface IConnectionProvider
    {
        public SqliteConnection GetConnection();
    }
}