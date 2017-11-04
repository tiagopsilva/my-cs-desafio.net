using System;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Entities;
using FluentValidation;

namespace AuthAPI.Domain.Validations
{
    public class TelefoneValidator : AbstractValidator<Telefone>
    {
        public TelefoneValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UsuarioId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Ddd)
                .NotEmpty()
                .Length(2, TelefoneSchemaInfo.DddMaxLength);

            RuleFor(x => x.Numero)
                .NotEmpty()
                .Length(8, TelefoneSchemaInfo.TelefoneMaxLength);
        }
    }
}