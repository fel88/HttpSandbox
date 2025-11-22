using Dapper;
using Npgsql;

namespace HttpSandbox
{
    public class DataAccessService
    {
        private readonly string _connectionString;

        public DataAccessService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> QueryRawSqlAsync<T>(string sql, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<T>(sql, parameters);
                return results;
            }
        }
    }
}
