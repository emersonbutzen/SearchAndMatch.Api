using MediatR;

namespace SearchAndMatch.Api.Commands
{
    public interface ICommand<T> : IRequest<T>
    { }
}