using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repositories.Interfaces;
using AuthAPI.Domain.ValueObjects;
using FirebirdSql.Data.FirebirdClient;

namespace AuthAPI.Infra.Data.ADO.NET
{
    public class SignUpRepository : AbstractADORepository, ISignUpRepository
    {
        public SignUpRepository(AuthApiDbConnection connection)
            : base(connection)
        {
        }

        public async Task<bool> EmailCadastradoAsync(string email)
        {
            const string sql = "select coalesce(count(*), 0) from \"Usuarios\" where lower(\"Email\") like lower(@Email)";

            var result = await Connection.ExecuteCountAsync(sql, new FbParameter("Email", email)) > 0;

            return result;
        }

        public async Task<MethodResult> InserirAsync(Usuario usuario)
        {
            const string sql = " insert into \"Usuarios\" (\"Id\", \"Nome\", \"Email\", \"Senha\", \"DataCriacao\") " +
                               " values (@Id, @Nome, @Email, @Senha, @DataCriacao) ";

            var sucesso = await Connection.ExecuteCommandAsync(sql,
                new FbParameter("Id", usuario.Id),
                new FbParameter("Nome", usuario.Nome),
                new FbParameter("Email", usuario.Email),
                new FbParameter("Senha", usuario.Senha),
                new FbParameter("DataCriacao", usuario.DataCriacao)) > 0;

            if (sucesso)
                return Success();

            return Error("Não foi possível inserir o registro");
        }

        public async Task<MethodResult> AlterarAsync(Usuario usuario)
        {
            const string sql = " update \"Usuarios\" set " +
                               " \"DataUpdate\" = @DataUpdate, " +
                               " \"DataUltimoLogin\" = @DataUltimoLogin, " +
                               " \"Token\" = @Token " +
                               " where \"Id\" = @Id ";

            var sucesso = await Connection.ExecuteCommandAsync(sql, 
                new FbParameter("DataUpdate", usuario.DataUpdate),
                new FbParameter("DataUltimoLogin", usuario.DataUltimoLogin),
                new FbParameter("Token", usuario.Token),
                new FbParameter("Id", usuario.Id)) > 0;

            if (sucesso)
                return Success();

            return Error("Não foi possível inserir o registro");
        }
    }
}