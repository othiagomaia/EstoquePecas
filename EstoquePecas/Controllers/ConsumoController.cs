using Dapper;
using EstoquePecas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EstoquePecas.Controllers
{
    public class ConsumoController : Controller
    {
        [ApiController]
        [Route("api/consumo")]
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
                    const string sql = "select * from consumo where ativo = 1;";
                    var consumos = await connection.QueryAsync<ConsumoModel>(sql);
                    return Ok(consumos);
                }

            }


            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var parameters = new { id };

                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string sql = "select * from consumo where id_consumo = @id";

                    var consumo = await connection.QuerySingleOrDefaultAsync<ConsumoModel>(sql, parameters);

                    if (consumo is null)
                    {
                        return NotFound();
                    }

                    return Ok(consumo);
                }
            }

            [HttpPost]
            public async Task<IActionResult> Post(ConsumoModel consumo)
            {
                var parameters = new
                {
                    consumo.Id_Peca,
                    consumo.Quantidade
                };

                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string sql = "insert into consumo(quantidade, id_peca) values (@Quantidade, @Id_Peca); select Last_Insert_Id();";

                    int createdId = await connection.ExecuteScalarAsync<int>(sql, parameters);

                    return Ok(createdId);
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, ConsumoModel consumo)
            {
                var parameters = new
                {
                    id,
                    consumo.Id_Peca,
                    consumo.Quantidade
                };

                using (var connection = new MySqlConnection(_connectionString))
                {
                    const string sql = "update consumo set id_peca = @Id_Peca, quantidade = @Quantidade where id_consumo = @id;";

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
                    const string sql = "update consumo set ativo = 0 where id_consumo = @id;";

                    await connection.ExecuteAsync(sql, parameters);

                    return NoContent();
                }
            }
        }
    }
}
