using MediatR;

namespace EstoquePecas.Commands
{
    public record DeletePecaCommand(int Id) : IRequest;
}
