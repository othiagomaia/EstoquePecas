using Dapper;
using EstoquePecas.Models;
using MediatR;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Commands
{
    public class PutPecaCommandHandler : IRequestHandler<PutPecaCommand>
    {
        private readonly string? _connectionString;

        public PutPecaCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task Handle(PutPecaCommand request, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                request.Id,
                request.Descricao,
                request.Saldo,
                request.Part_Number,
                request.Preco_Medio
            };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "update pecas set descricao = @Descricao, saldo = @Saldo, part_number = @Part_Number, preco_medio = @Preco_Medio where id_peca = @Id;";

                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
