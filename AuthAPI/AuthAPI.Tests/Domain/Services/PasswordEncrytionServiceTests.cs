using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthAPI.Domain.Services;

namespace AuthAPI.Tests.Domain.Services
{
    [TestClass]
    public class PasswordEncryptionServiceTests
    {
        [TestMethod]
        public void DeveEncriptarTexto()
        {
            var texto = Guid.NewGuid().ToString();
            var hash = new PasswordEncryptionService().EncryptPassword(texto);
            Assert.AreNotEqual(texto, hash);
        }

        [TestMethod]
        public void DeveGerarHashesIguaisVariasVezes()
        {
            var texto = Guid.NewGuid().ToString();
            var hash1 = new PasswordEncryptionService().EncryptPassword(texto);
            var hash2 = new PasswordEncryptionService().EncryptPassword(texto);

            var passwordEncryptionService = new PasswordEncryptionService();
            var hash3 = passwordEncryptionService.EncryptPassword(texto);
            var hash4 = passwordEncryptionService.EncryptPassword(texto);

            Assert.AreEqual(hash1, hash2, "Hash 1 e 2 são diferentes");
            Assert.AreEqual(hash3, hash4, "Hash 3 e 4 são diferentes");
            Assert.AreEqual(hash1, hash3, "Hash 1 e 3 são diferentes");
            Assert.AreEqual(hash1, hash4, "Hash 1 e 4 são diferentes");
            Assert.AreEqual(hash2, hash3, "Hash 2 e 3 são diferentes");
            Assert.AreEqual(hash2, hash4, "Hash 2 e 4 são diferentes");
        }

        [TestMethod]
        public void DeveReconhecerSenhaEncriptada()
        {
            var texto = Guid.NewGuid().ToString();
            var encryptionService = new PasswordEncryptionService();
            var hash = encryptionService.EncryptPassword(texto);
            Assert.IsTrue(encryptionService.ComparePasswords(texto, hash));
        }
    }
}