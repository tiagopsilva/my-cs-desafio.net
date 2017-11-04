using System.Threading.Tasks;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Commands.Interfaces;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Mapper;
using AuthAPI.Domain.Services;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Commands.Handlers
{
    public class SignUpCommandHandler : AbstractCommandHandler,
        ICommandHandler<CriarUsuarioCommand>
    {
        private readonly SignUpService _service;

        public SignUpCommandHandler(SignUpService service)
        {
            _service = service;
        }

        public async Task<MethodResult> ExecuteAsync(CriarUsuarioCommand command)
        {
            if (command == null)
                return Error(Messages.ComandoInvalido);

            var usuario = new UsuarioMapper().MapFrom(command);

            var result = await _service.InserirAsync(usuario);
            if (result.Failed)
                return result;

            var output = new UsuarioMapper().OutputFrom(usuario);
            return Success(output);
        }
    }
}