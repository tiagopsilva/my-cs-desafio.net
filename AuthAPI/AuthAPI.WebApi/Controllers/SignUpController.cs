using System.Threading.Tasks;
using System.Web.Http;
using AuthAPI.Domain.Commands.Handlers;
using AuthAPI.Domain.Commands.Inputs.Usuarios;

namespace AuthAPI.WebApi.Controllers
{
    public class SignUpController : AbstractController
    {
        private readonly SignUpCommandHandler _commandHandler;

        public SignUpController(SignUpCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CriarUsuarioCommand command)
        {
            var result = await _commandHandler.ExecuteAsync(command);
            return ProcessResult(result);
        }
    }
}