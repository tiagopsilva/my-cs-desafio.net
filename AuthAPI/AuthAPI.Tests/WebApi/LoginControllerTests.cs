using System;
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
    public class LoginControllerTests : AbstractControllerTest
    {
        private readonly string _actionName = nameof(LoginController).Replace("Controller", "");
        private readonly string _signUpActionName = nameof(SignUpController).Replace("Controller", "");

        [TestMethod]
        public async Task DeveLogarERetornarDadosDeUsuarioCadastrado()
        {
            var nome = Guid.NewGuid().ToString("N");
            var email = $"{Guid.NewGuid():N}@test.io";
            var senha = Guid.NewGuid().ToString();

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_signUpActionName, new CriarUsuarioCommand(nome, email, senha));
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);
            }

            using (var cliente = CreateClient())
            {
                var response = await cliente.PostAsJsonAsync(_actionName, new LogarUsuarioCommand(email, senha));
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);
                Assert.AreEqual(nome, usuario.Nome);
                Assert.AreEqual(email, usuario.Email);
            }
        }

        [TestMethod]
        public async Task NaoDeveLogarUsuarioNaoCadastrado()
        {
            var email = $"{Guid.NewGuid():N}@test.io";
            var senha = Guid.NewGuid().ToString();

            using (var cliente = CreateClient())
            {
                var response = await cliente.PostAsJsonAsync(_actionName, new LogarUsuarioCommand(email, senha));
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

                var error = await response.Content.ReadAsAsync<ErrorResultModel>();
                Assert.AreEqual((int)HttpStatusCode.Unauthorized, error.StatusCode);
                Assert.AreEqual(Messages.UsuarioNaoCadastrado, error.Mensagem);
            }
        }

        [TestMethod]
        public async Task NaoDeveLogarUsuarioComSenhaErrada()
        {
            var nome = Guid.NewGuid().ToString("N");
            var email = $"{Guid.NewGuid():N}@test.io";
            var senha = Guid.NewGuid().ToString();

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_signUpActionName, new CriarUsuarioCommand(nome, email, senha));
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);
            }

            using (var cliente = CreateClient())
            {
                var response = await cliente.PostAsJsonAsync(_actionName, new LogarUsuarioCommand(email, Guid.NewGuid().ToString()));
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);

                var error = await response.Content.ReadAsAsync<ErrorResultModel>();
                Assert.AreEqual((int)HttpStatusCode.Unauthorized, error.StatusCode);
                Assert.AreEqual(Messages.UsuarioNaoCadastrado, error.Mensagem);
            }
        }
    }
}