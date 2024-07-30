using Dapper;
using EstoquePecas.Models;
using MediatR;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Queries
{
    public class GetPecaByIdQueryHandler : IRequestHandler<GetPecaByIdQuery, Peca?>
    {
        private readonly string? _connectionString;

        public GetPecaByIdQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<Peca?> Handle(GetPecaByIdQuery request, CancellationToken cancellationToken)
        {
            var parameters = new { request.Id };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "select * from pecas where id_peca = @id";

                var peca = await connection.QuerySingleOrDefaultAsync<Peca?>(sql, parameters);

                return peca;
            }
        }
    }
}
