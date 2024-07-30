using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using EstoquePecas.Models;
using Microsoft.OpenApi.Validations;
using MediatR;
using EstoquePecas.Commands;
using EstoquePecas.Queries;

namespace EstoquePecas.Controllers
{
    [ApiController]
    [Route("api/pecas")]
    public class PecaController : ControllerBase
    {

        private readonly string? _connectionString;
        private readonly IMediator _mediator;

        public PecaController(IConfiguration configuration, IMediator mediator)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _mediator = mediator;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pecas = await _mediator.Send(new GetPecasQuery());
            return Ok(pecas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var peca = await _mediator.Send(new GetPecaByIdQuery(id));

            if (peca is null)
                return NotFound();
            
            return Ok(peca);
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPecaCommand command)
        {
            int createdId = await _mediator.Send(command);
            return Ok(createdId);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PutPecaCommand command)
        {
            await _mediator.Send(command);
            return Ok();
                
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePecaCommand(id));
            return Ok();
        }

    }
}
