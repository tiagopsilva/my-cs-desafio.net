using System;
using System.Threading.Tasks;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Extensions;
using AuthAPI.Domain.Repositories.Interfaces;
using AuthAPI.Domain.Services.Interfaces;
using AuthAPI.Domain.Validations;
using AuthAPI.Domain.ValueObjects;

namespace AuthAPI.Domain.Services
{
    public class SignUpService
    {
        private readonly ISignUpRepository _repository;
        private readonly UsuarioValidator _validator;
        private readonly ITokenService _tokenService;

        public SignUpService(ISignUpRepository repository, UsuarioValidator validator, ITokenService tokenService)
        {
            _repository = repository;
            _validator = validator;
            _tokenService = tokenService;
        }

        public async Task<MethodResult> InserirAsync(Usuario usuario)
        {
            var validationResult = await _validator.ValidateAsync(usuario);
            if (!validationResult.IsValid)
                return new MethodResult(validationResult.Errors);

            if (usuario.DataCriacao < DateTime.Now.AddSeconds(-60))
                return new MethodResult($"A Data de Criação {usuario.DataCriacao:G} é inválida");

            if (await _repository.EmailCadastradoAsync(usuario.Email))
                return new MethodResult(Messages.EmailExistente);

            var result = await _repository.InserirAsync(usuario);
            if (result.Failed)
                return result;

            return await GravarLoginAsync(usuario);
        }

        public async Task<MethodResult> GravarLoginAsync(Usuario usuario)
        {
            usuario.DataUltimoLogin = DateTime.Now;
            usuario.DataUpdate = usuario.DataUltimoLogin;

            usuario.Token = _tokenService.GenerateToken(usuario);

            if (usuario.Token.IsEmpty())
                return new MethodResult(Messages.TokenNaoEncontrado);

            var validationResult = await _validator.ValidateAsync(usuario);
            if (!validationResult.IsValid)
                return new MethodResult(validationResult.Errors);

            return await _repository.AlterarAsync(usuario);
        }
    }
}