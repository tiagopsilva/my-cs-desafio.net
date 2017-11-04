using System.Threading.Tasks;
using System.Web.Http;
using AuthAPI.Domain.Commands.Handlers;
using AuthAPI.Domain.Commands.Inputs.Usuarios;

namespace AuthAPI.WebApi.Controllers
{
    public class LoginController : AbstractController
    {
        private readonly UsuarioCommandHandler _commandHandler;

        public LoginController(UsuarioCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(LogarUsuarioCommand command)
        {
            var result = await _commandHandler.ExecuteAsync(command);
            return ProcessResult(result);
        }
    }
}