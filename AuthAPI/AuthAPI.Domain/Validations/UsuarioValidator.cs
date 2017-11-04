using System;
using AuthAPI.Domain.Entities;
using FluentValidation;

namespace AuthAPI.Domain.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(5, 255);

            RuleFor(x => x.Senha)
                .NotEmpty();

            RuleFor(x => x.DataCriacao);

            RuleFor(x => x.DataUpdate);

            RuleFor(x => x.DataUltimoLogin);

            RuleFor(x => x.Token);

            RuleForEach(x => x.Telefones)
                .NotNull();

            RuleFor(x => x.Telefones)
                .SetCollectionValidator(new TelefoneValidator());
        }
    }
}