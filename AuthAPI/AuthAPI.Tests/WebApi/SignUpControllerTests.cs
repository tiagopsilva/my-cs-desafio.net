using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Commands.Outputs.Usuarios;
using AuthAPI.Domain.Constants;
using AuthAPI.WebApi.Controllers;
using AuthAPI.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.WebApi
{
    [TestClass]
    public class SignUpControllerTests : AbstractControllerTest
    {
        private readonly string _actionName = nameof(SignUpController).Replace("Controller", "");

        [TestMethod]
        public async Task DeveCadastrarUsuarioValido()
        {
            var nome = Guid.NewGuid().ToString();
            var email = $"{DateTime.Now:yyyyMMddhhmmss}@test.io";
            var senha = Guid.NewGuid().ToString("N");

            var criarUsuarioCommand = new CriarUsuarioCommand(nome, email, senha);

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_actionName, criarUsuarioCommand);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

                var usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);

                Assert.AreNotEqual(Guid.Empty, usuario.Id);
                Assert.AreEqual(nome, usuario.Nome);
                Assert.AreEqual(email, usuario.Email);
                Assert.AreEqual("***", usuario.Senha);
                Assert.IsNotNull(usuario.DataUltimoLogin);
                Assert.IsNotNull(usuario.DataUpdate);
                Assert.IsNotNull(usuario.Token);
                Assert.AreEqual(0, usuario.Telefones.Count);
            }
        }

        [TestMethod]
        public async Task DeveRecusarEmailInvalido()
        {
            var nome = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString("N");
            var senha = Guid.NewGuid().ToString("N");

            var criarUsuarioCommand = new CriarUsuarioCommand(nome, email, senha);

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_actionName, criarUsuarioCommand);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);

                var error = await response.Content.ReadAsAsync<ErrorResultModel>();
                Assert.IsNotNull(error);

                Assert.IsNotNull(error.StatusCode == (int)HttpStatusCode.Unauthorized);
                Assert.IsTrue(error.Mensagem.Any());
            }
        }

        [TestMethod]
        public async Task DeveRecusarEmailJaExistente()
        {
            var nome = Guid.NewGuid().ToString();
            var email = $"{Guid.NewGuid():N}@test.io";
            var senha = Guid.NewGuid().ToString("N");

            var criarUsuarioCommand = new CriarUsuarioCommand(nome, email, senha);

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_actionName, criarUsuarioCommand);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

                var usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);
            }

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_actionName, criarUsuarioCommand);
                Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);

                var error = await response.Content.ReadAsAsync<ErrorResultModel>();
                Assert.IsNotNull(error);

                Assert.AreEqual((int)HttpStatusCode.Unauthorized, error.StatusCode);
                Assert.AreEqual(Messages.EmailExistente, error.Mensagem);
            }
        }
    }
}