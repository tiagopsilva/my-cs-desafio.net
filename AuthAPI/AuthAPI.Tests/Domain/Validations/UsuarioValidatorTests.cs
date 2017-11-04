using System;
using System.Linq;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Services;
using AuthAPI.Domain.Validations;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Validations
{
    [TestClass]
    public class UsuarioValidatorTests
    {
        private const string NOME = "Fulaninho";
        private const string EMAIL = "email@unittest.edu";
        private const string PASSWORD = "^querty@123";

        private const string DDD1 = "011";
        private const string NUMERO1 = "99999999";

        private const string DDD2 = "013";
        private const string NUMERO2 = "88888888";

        private readonly UsuarioValidator _validator;

        public UsuarioValidatorTests()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            _validator = new UsuarioValidator();
        }

        [TestMethod]
        public void DeveAprovarUsuarioCompleto()
        {
            var usuario = new Usuario(NOME, EMAIL, PASSWORD)
            {
                DataCriacao = DateTime.Now                 
            };

            var telefone1 = new Telefone(usuario.Id, DDD1, NUMERO1);
            var telefone2 = new Telefone(usuario.Id, DDD2, NUMERO2);
            usuario.Telefones.Add(telefone1);
            usuario.Telefones.Add(telefone2);

            var validationResult = _validator.Validate(usuario);

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(NOME, usuario.Nome);
            Assert.AreEqual(EMAIL, usuario.Email);
            Assert.IsTrue(new PasswordEncryptionService().ComparePasswords(PASSWORD, usuario.Senha));

            Assert.IsNotNull(usuario.Telefones);
            Assert.AreEqual(2, usuario.Telefones.Count);

            Assert.AreEqual(DDD1, usuario.Telefones.First().Ddd);
            Assert.AreEqual(NUMERO1, usuario.Telefones.First().Numero);

            Assert.AreEqual(DDD2, usuario.Telefones.Last().Ddd);
            Assert.AreEqual(NUMERO2, usuario.Telefones.Last().Numero);
        }

        [TestMethod]
        public void DeveReprovarUsuarioComTelefoneInvalido()
        {
            var usuario = new Usuario(NOME, EMAIL, PASSWORD)
            {
                DataCriacao = DateTime.Now
            };

            usuario.Telefones.Add(new Telefone(usuario.Id, "", ""));

            var validationResult = _validator.Validate(usuario);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Any());

            Assert.AreEqual(2, validationResult.Errors.Count);
        }
    }
}