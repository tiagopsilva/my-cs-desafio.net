using System.Collections.Generic;
using AuthAPI.Domain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Extensions
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void DeveReconhecerComoVazioQuandoObjetoEstaNulo()
        {
            var estaVazio = ((IEnumerable<object>)null).IsEmpty();
            Assert.IsTrue(estaVazio);
        }

        [TestMethod]
        public void DeveReconhercerStringComoVaziaQuandoNaoHouverCaracteres()
        {
            var estaVazio = string.Empty.IsEmpty();
            Assert.IsTrue(estaVazio);
        }

        [TestMethod]
        public void DeveReconherComoVazioQuandoListaNaoPossuiItens()
        {
            var estaVazio = new List<object>().IsEmpty();
            Assert.IsTrue(estaVazio);
        }

        [TestMethod]
        public void DeveReconhercerComoPopuladoQuandoListaPossuirItem()
        {
            var lista = new List<string> { "Deve passar" };
            var estaVazio = lista.IsEmpty();
            Assert.IsFalse(estaVazio);
        }
    }
}