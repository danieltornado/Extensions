using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Sqlite;

public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Sets using Sqlite in memory
    /// </summary>
    /// <param name="optionsBuilder"></param>
    [PublicAPI]
    public static DbConnection UseSqliteInMemory(this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder.UseSqliteInMemory(e => { });
    }

    /// <summary>
    /// Sets using Sqlite in memory
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /// <param name="configure"></param>
    [PublicAPI]
    public static DbConnection UseSqliteInMemory(this DbContextOptionsBuilder optionsBuilder, Action<SqliteConnectionStringBuilder> configure)
    {
        SqliteConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.Mode = SqliteOpenMode.Memory;
        connectionStringBuilder.Cache = SqliteCacheMode.Shared;
        connectionStringBuilder.DataSource = ":memory:";

        configure(connectionStringBuilder);

        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        optionsBuilder.UseSqlite(connection);

        return connection;
    }
}