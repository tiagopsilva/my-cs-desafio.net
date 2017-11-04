using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repositories.Interfaces;
using AuthAPI.Domain.ValueObjects;
using AuthAPI.Infra.Data.EF.EFContext;

namespace AuthAPI.Infra.Data.EF.Repositories
{
    public class UsuarioRepository : AbstractEFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AuthAPIContext context)
            :base(context)
        {
        }

        public Task<Usuario> ObterPeloIdAsync(Guid id, params Expression<Func<Usuario, object>>[] includes)
        {
            IQueryable<Usuario> query = Set;

            foreach (var include in includes ?? new Expression<Func<Usuario, object>>[] { })
                query = query.Include(include);

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<Usuario> ObterPeloEmailAsync(string email, params Expression<Func<Usuario, object>>[] includes)
        {
            IQueryable<Usuario> query = Set;

            foreach (var include in includes ?? new Expression<Func<Usuario, object>>[] { })
                query = query.Include(include);

            return query.SingleOrDefaultAsync(x => x.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public Task<bool> EmailCadastradoAsync(string email)
        {
            return Set.AsNoTracking().Where(x => x != null).AnyAsync(x => x.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public Task<MethodResult> InserirAsync(Usuario usuario)
        {
            return AddAsync(usuario);
        }

        public Task<MethodResult> AlterarAsync(Usuario usuario)
        {
            return UpdateAsync(usuario);
        }
    }
}