using System;
using AuthAPI.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Entities
{
    [TestClass]
    public class TelefoneTests
    {
        private const string DDD = "011";
        private const string NUMERO = "99999999";

        [TestMethod]
        public void DeveCriarTelefoneComPropriedadesPassadasNoConstrutorMaisId()
        {
            var usuarioId = Guid.NewGuid();

            var telefone = new Telefone(usuarioId, DDD, NUMERO);

            Assert.IsTrue(Guid.Empty != telefone.Id);
            Assert.IsTrue(usuarioId == telefone.UsuarioId);
            Assert.AreEqual(DDD, telefone.Ddd);
            Assert.AreEqual(NUMERO, telefone.Numero);
        }

        [TestMethod]
        public void DeveArmazenarSomenteNumeros()
        {
            var telefone = new Telefone(Guid.NewGuid(), $"({DDD})", NUMERO.Insert(4, "-"));
            Assert.AreEqual(DDD, telefone.Ddd);
            Assert.AreEqual(NUMERO, telefone.Numero);
        }
    }
}