using EstoquePecas.Models;
using MediatR;

namespace EstoquePecas.Queries
{
    public record GetPecaByIdQuery(int Id) : IRequest<Peca?>;
}
