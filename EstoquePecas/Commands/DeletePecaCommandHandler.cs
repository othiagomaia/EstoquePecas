using Dapper;
using MediatR;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Commands
{
    public class DeletePecaCommandHandler : IRequestHandler<DeletePecaCommand>
    {
        private readonly string? _connectionString;

        public DeletePecaCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task Handle(DeletePecaCommand request, CancellationToken cancellationToken)
        {
            var parameters = new { request.Id };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "update pecas set ativa = 0 where id_peca = @Id;";

                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
