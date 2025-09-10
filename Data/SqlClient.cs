using checkpoint01.Model;
using Dapper;
using MySql.Data.MySqlClient;

namespace checkpoint01.Data
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(string id)
        {
            using (var connection = new MySqlConnection(_connectionString)) 
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Nome, Email, UltimoAcesso FROM Usuarios WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
            }
        }
    }
}
