using Dapper;
using EstoquePecas.Models;
using MediatR;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Queries
{
    public class GetPecasQueryHandler : IRequestHandler<GetPecasQuery, IEnumerable<Peca>>
    {
        private readonly string? _connectionString;

        public GetPecasQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<Peca>> Handle(GetPecasQuery request, CancellationToken cancellationToken)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "select id_peca, descricao, saldo, preco_medio, part_number from pecas where ativa = 1;";
                var pecas = await connection.QueryAsync<Peca>(sql);
                return pecas;
            }
        }
    }
}
