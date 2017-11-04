using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AuthAPI.Domain.Commands.Inputs.Usuarios;
using AuthAPI.Domain.Commands.Outputs.Usuarios;
using AuthAPI.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthAPI.Tests.WebApi
{
    [TestClass]
    public class Profile1ControllerTests : AbstractControllerTest
    {
        private readonly string _actionName = nameof(Profile1Controller).Replace("Controller", "");
        private readonly string _signUpActionName = nameof(SignUpController).Replace("Controller", "");

        [TestMethod]
        public async Task DeveObterDadosDoUsuarioLogado()
        {
            var nome = Guid.NewGuid().ToString();
            var email = $"{DateTime.Now:yyyyMMddhhmmss}@test.io";
            var senha = Guid.NewGuid().ToString("N");

            UsuarioOutputCommand usuario;

            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(_signUpActionName, new CriarUsuarioCommand(nome, email, senha));
                Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

                usuario = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(usuario);
            }

            using (var client = CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var response = await client.GetAsync($"{_actionName}/{usuario.Id}");
                var profile = await response.Content.ReadAsAsync<UsuarioOutputCommand>();
                Assert.IsNotNull(profile);

                Assert.AreEqual(usuario.Id, profile.Id);
                Assert.AreEqual(usuario.Nome, profile.Nome);
                Assert.AreEqual(usuario.Email, profile.Email);
            }
        }
    }
}