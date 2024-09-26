using Dapper;
using Microsoft.Data.SqlClient;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeDatabase()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string sqlCreateDatabase = @"
                                        IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Chamados_DEV')
                                        BEGIN
                                            CREATE DATABASE Chamados_DEV;
                                        END
                                    ";

            await connection.ExecuteAsync(sqlCreateDatabase);
        }

        var connectionStringDatabase = _connectionString.Replace("Database=master;", "Database=Chamados_DEV;");

        using (var connection = new SqlConnection(connectionStringDatabase))
        {
            await connection.OpenAsync();

            var sqlCreateTable = @"
                                    IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'CHAMADOS' AND xtype = 'U')
                                    BEGIN
                                        CREATE TABLE CHAMADOS (
                                            ID INT NOT NULL PRIMARY KEY,
                                            TITULO NVARCHAR(50),
                                            AREA NVARCHAR(50),
                                            DATAABERTURA NVARCHAR(12),
                                            SITUACAO NVARCHAR(50)
                                        );
                                    END
                                ";

            await connection.ExecuteAsync(sqlCreateTable);
        }
    }
}