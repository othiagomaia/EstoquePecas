using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EstoquePecas.Commands
{
    public record PostPecaCommand(string Descricao, int Saldo, decimal Preco_Medio, int Part_Number) : IRequest<int>;
}
