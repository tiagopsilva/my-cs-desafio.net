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
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioValidator _validator;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repository, UsuarioValidator validator, ITokenService tokenService)
        {
            _repository = repository;
            _validator = validator;
            _tokenService = tokenService;
        }

        public Task<Usuario> ObterPeloIdAsync(Guid id)
        {
            return _repository.ObterPeloIdAsync(id);
        }

        public Task<Usuario> ObterPeloEmailAsync(string email)
        {
            return _repository.ObterPeloEmailAsync(email);
        }

        public Task<bool> EmailCadastradoAsync(string email)
        {
            return _repository.EmailCadastradoAsync(email);
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