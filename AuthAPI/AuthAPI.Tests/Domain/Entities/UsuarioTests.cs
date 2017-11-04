using System;
using System.Linq;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Entities
{
    [TestClass]
    public class UsuarioTests
    {
        private const string NOME = "Fulano";
        private const string EMAIL = "email@teste.io";
        private const string PASSWORD = "123456";

        [TestMethod]
        public void DeveCriarUsuarioComParametrosPassadosNoContrutorIdEListaDeTelefonesCriada()
        {
            var usuario = new Usuario(NOME, EMAIL, PASSWORD);
            var encryptedPassword = new PasswordEncryptionService().EncryptPassword(PASSWORD);

            Assert.IsTrue(Guid.Empty != usuario.Id);
            Assert.IsFalse(usuario.Telefones == null);
            Assert.IsFalse(usuario.Telefones.Any());
            Assert.AreEqual(NOME, usuario.Nome);
            Assert.AreEqual(EMAIL, usuario.Email);
            Assert.AreEqual(encryptedPassword, usuario.Senha);
        }
    }
}