using System;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Validations;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Validations
{
    [TestClass]
    public class TelefoneValidatorTests
    {
        private const string DDD = "011";
        private const string NUMERO = "99999999";

        public TelefoneValidatorTests()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }

        [TestMethod]
        public void DeveAprovarTelefoneCompleto()
        {
            var validator = new TelefoneValidator();
            var telefone = new Telefone(Guid.NewGuid(), $"({DDD})", NUMERO.Insert(4, "-"));

            var validationResult = validator.Validate(telefone);

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(DDD, telefone.Ddd);
            Assert.AreEqual(NUMERO, telefone.Numero);
        }

        [TestMethod]
        public void DeveReprovarTelefoneVazio()
        {
            var validator = new TelefoneValidator();
            var telefone = new Telefone(Guid.Empty, "", "");

            var validationResult = validator.Validate(telefone);

            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(3, validationResult.Errors.Count);
        }
    }
}