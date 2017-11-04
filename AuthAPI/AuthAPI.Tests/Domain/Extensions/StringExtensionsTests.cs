using System;
using AuthAPI.Domain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void DeveRetornarSomenteOsNumerosDaString()
        {
            var numeros = "ABCDEFG0123456hijklmno789".OnlyNumbers();
            Assert.AreEqual("0123456789", numeros);
        }

        [TestMethod]
        public void DeveRetornarVazioQuandoStringEstiverNula()
        {
            var numeros = ((string)null).OnlyNumbers();
            Assert.IsTrue(numeros == string.Empty);
        }

        [TestMethod]
        public void DeveRetornarVazioQuandoStringEstiverVazia()
        {
            var numeros = "".OnlyNumbers();
            Assert.IsTrue(numeros == string.Empty);
        }

        [TestMethod]
        public void DeveRetornarVerdadeiroQuandoStringsSaoIguais()
        {
            var valor = Guid.NewGuid().ToString("N");
            Assert.IsTrue(valor.EqualsIgnoreCase(valor));
        }

        [TestMethod]
        public void DeveRetornarVerdadeiroIgnorandoCaseSensitiveQuandoStringsSaoIguais()
        {
            var guid = Guid.NewGuid();
            var valor1 = guid.ToString("N");
            var valor2 = valor1.ToUpper();
            Assert.IsTrue(valor1.EqualsIgnoreCase(valor2));
        }

        [TestMethod]
        public void DeveRetornarFalsoQuandoStringsForemDiferentes()
        {
            var guid = Guid.NewGuid();
            var valor1 = guid.ToString();
            var valor2 = valor1 + " ";
            Assert.IsFalse(valor1.EqualsIgnoreCase(valor2));
        }

        [TestMethod]
        public void DeveSerCapazDeCompararUmaStringNula()
        {
            Assert.IsFalse(((string)null).EqualsIgnoreCase(""));
        }
    }
}