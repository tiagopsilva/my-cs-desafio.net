using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPeloIdAsync(Guid id, params Expression<Func<Usuario, object>>[] includes);
        Task<Usuario> ObterPeloEmailAsync(string email, params Expression<Func<Usuario, object>>[] includes);
        Task<bool> EmailCadastradoAsync(string email);
        Task<MethodResult> AlterarAsync(Usuario entity);
    }
}