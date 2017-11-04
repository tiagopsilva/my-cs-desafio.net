using System;
using System.Linq;
using AuthAPI.Domain.Commands.Inputs.Telefones;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Extensions;
using AuthAPI.Domain.Mapper;
using AuthAPI.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.Domain.Mapper
{
    [TestClass]
    public class UsuarioMapperTests
    {
        private const string NOME = "Qualquer";
        private const string EMAIL = "email@teste.me";
        private const string SENHA = "qwerty789";

        private const string DDD1 = "011";
        private const string TELEFONE1 = "99999999";

        private const string DDD2 = "013";
        private const string TELEFONE2 = "8888-8888";

        [TestMethod]
        public void DeveCriarUsuarioComDataDeCriacaoETelefones()
        {
            var criarUsuarioCommand = new CriarUsuarioCommand(NOME, EMAIL, SENHA);

            criarUsuarioCommand.Telefones.Add(new CriarTelefoneCommand(DDD1, TELEFONE1));
            criarUsuarioCommand.Telefones.Add(new CriarTelefoneCommand($"({DDD2})", TELEFONE2));

            var usuario = new UsuarioMapper().MapFrom(criarUsuarioCommand);

            Assert.IsNotNull(usuario);
            Assert.AreNotEqual(Guid.Empty, usuario.Id);
            Assert.IsNotNull(usuario.Telefones);
            Assert.IsTrue(usuario.Telefones.Any());
            Assert.AreEqual(2, usuario.Telefones.Count);

            Assert.AreEqual(NOME, usuario.Nome);
            Assert.AreEqual(EMAIL, usuario.Email);
            Assert.IsTrue(new PasswordEncryptionService().ComparePasswords(SENHA, usuario.Senha));

            Assert.AreEqual(usuario.Telefones.First().Ddd, DDD1);
            Assert.AreEqual(usuario.Telefones.First().Numero, TELEFONE1);

            Assert.AreEqual(usuario.Telefones.Last().Ddd, DDD2.OnlyNumbers());
            Assert.AreEqual(usuario.Telefones.Last().Numero, TELEFONE2.OnlyNumbers());
        }
    }
}