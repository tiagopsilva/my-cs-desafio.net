using System.Threading.Tasks;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Commands.Interfaces
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<MethodResult> ExecuteAsync(TCommand command);
    }
}