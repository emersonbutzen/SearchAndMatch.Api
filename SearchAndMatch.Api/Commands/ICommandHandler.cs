using MediatR;

namespace SearchAndMatch.Api.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { }
}