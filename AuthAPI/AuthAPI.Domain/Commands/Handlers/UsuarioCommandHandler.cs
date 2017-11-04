using System.Net;
using System.Threading.Tasks;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Commands.Interfaces;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Mapper;
using AuthAPI.Domain.Services;
using AuthAPI.Domain.Validations;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Commands.Handlers
{
    public class UsuarioCommandHandler : AbstractCommandHandler,
        ICommandHandler<ObterUsuarioPeloIdCommand>,
        ICommandHandler<LogarUsuarioCommand>
    {
        private readonly UsuarioService _service;
        private readonly EmailValidator _emailValidator;

        public UsuarioCommandHandler(UsuarioService service, EmailValidator emailValidator)
        {
            _service = service;
            _emailValidator = emailValidator;
        }

        public async Task<MethodResult> ExecuteAsync(ObterUsuarioPeloIdCommand command)
        {
            if (command == null)
                return Error(Messages.ComandoInvalido);

            var usuario = await _service.ObterPeloIdAsync(command.Id);
            if (usuario == null)
                return Error(Messages.UsuarioNaoCadastrado);

            var output = new UsuarioMapper().OutputFrom(usuario);
            return Success(output);
        }

        public async Task<MethodResult> ExecuteAsync(LogarUsuarioCommand command)
        {
            if (command == null)
                return Error(Messages.ComandoInvalido);

            var emailValidationResult = _emailValidator.Validate(new Email(command.Email));
            if (!emailValidationResult.IsValid)
                return Error(emailValidationResult);

            var usuario = await _service.ObterPeloEmailAsync(command.Email);

            if (usuario == null)
                return Error(Messages.UsuarioNaoCadastrado);

            if (!usuario.ValidarSenha(command.Senha))
                return new MethodResult(Messages.UsuarioNaoCadastrado) { ErrorCode = HttpStatusCode.Unauthorized };

            var result = await _service.GravarLoginAsync(usuario);
            if (result.Failed)
                return result;

            var output = new UsuarioMapper().OutputFrom(usuario);
            return Success(output);
        }
    }
}
