using EstoquePecas.Models;
using MediatR;

namespace EstoquePecas.Queries
{
    public class GetPecasQuery : IRequest<IEnumerable<Peca>>
    {
    }
}
