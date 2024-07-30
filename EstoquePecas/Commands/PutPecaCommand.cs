using MediatR;

namespace EstoquePecas.Commands
{
    public record PutPecaCommand(int Id, string Descricao, int Saldo, decimal Preco_Medio, int Part_Number) : IRequest;
}
