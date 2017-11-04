using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Repositories.Interfaces
{
    public interface ISignUpRepository
    {
        Task<bool> EmailCadastradoAsync(string email);
        Task<MethodResult> InserirAsync(Usuario usuario);
        Task<MethodResult> AlterarAsync(Usuario usuario);
    }
}