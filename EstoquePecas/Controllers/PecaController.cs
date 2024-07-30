using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using EstoquePecas.Models;
using Microsoft.OpenApi.Validations;

namespace EstoquePecas.Controllers
{
    [ApiController]
    [Route("api/pecas")]
    public class PecaController : ControllerBase
    {

        private readonly string? _connectionString;

        public PecaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "select id_peca, descricao, saldo, preco_medio, part_number from pecas where ativa = 1;";
                var pecas = await connection.QueryAsync<PecaModel>(sql);
                return Ok(pecas);
            }     
           
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var parameters = new { id };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "select * from pecas where id_peca = @id";

                var peca = await connection.QuerySingleOrDefaultAsync<PecaModel>(sql, parameters);

                if (peca is null)
                {
                    return NotFound();
                }

                return Ok(peca);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PecaModel peca)
        {
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

                return Ok(createdId);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PecaModel peca)
        {
            var parameters = new
            {
                id,
                peca.Descricao,
                peca.Saldo,
                peca.Part_Number,
                peca.Preco_Medio
            };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "update pecas set descricao = @Descricao, saldo = @Saldo, part_number = @Part_Number, preco_medio = @Preco_Medio where id_peca = @id;";

                await connection.ExecuteAsync(sql, parameters);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameters = new { id };

            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "update pecas set ativa = 0 where id_peca = @id;";

                await connection.ExecuteAsync(sql, parameters);

                return NoContent();
            }
        }

    }
}
