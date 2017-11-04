using System;
using System.Threading.Tasks;
using System.Web.Http;
using AuthAPI.Domain.Commands.Handlers;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.WebApi.Filters;

namespace AuthAPI.WebApi.Controllers
{
    [JwtAuthorize]
    public class Profile1Controller : AbstractController
    {
        private readonly UsuarioCommandHandler _commandHandler;

        public Profile1Controller(UsuarioCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid? id)
        {
            var response = await _commandHandler.ExecuteAsync(new ObterUsuarioPeloIdCommand(id ?? Guid.Empty));
            return ProcessResult(response);
        }
    }
}