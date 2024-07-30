using Dapper;
using EstoquePecas.Models;
using MediatR;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Commands
{
    public class PostPecaCommandHandler : IRequestHandler<PostPecaCommand, int>
    {
        private readonly string? _connectionString;

        public PostPecaCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<int> Handle(PostPecaCommand request, CancellationToken cancellationToken)
        {
            var peca = new Peca { Descricao = request.Descricao, Part_Number = request.Part_Number, Preco_Medio = request.Preco_Medio, Saldo = request.Saldo };

            var parameters = new
            {
                peca.Descricao,
                peca.Saldo,
                peca.Part_Number,
                peca.Preco_Medio
            };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "insert into pecas(descricao, saldo, part_number, preco_medio) values (@Descricao, @Saldo, @Part_Number, @Preco_Medio); select Last_Insert_Id();";

                int createdId = await connection.ExecuteScalarAsync<int>(sql, parameters);

                return createdId;
            }
        }
    }
}
