using System;
using System.IdentityModel.Tokens;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using AuthAPI.Domain.Constants;
using AuthAPI.Domain.Extensions;
using AuthAPI.Domain.Services;

namespace AuthAPI.WebApi.Filters
{
    public sealed class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var token = actionContext.Request.Headers.Authorization?.Parameter;
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNaoEncontrado };
                return;
            }

            var service = Startup.Container.GetInstance<UsuarioService>();
            if (service == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                return;
            }

            var tokenDecoder = new JwtSecurityTokenHandler();
            var jwt = (JwtSecurityToken)tokenDecoder.ReadToken(token);

            if (jwt.Subject.IsEmpty())
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNaoEncontrado };
                return;
            }

            var usuario = await service.ObterPeloIdAsync(Guid.Parse(jwt.Subject));
            if (usuario == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNaoEncontrado };
                return;
            }

            if (!usuario.Token.Equals(token))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenNaoEncontrado };
                return;
            }

            if (usuario.DataUltimoLogin < DateTime.Now.AddMinutes(-30))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = Messages.TokenExpirado };
                return;
            }
        }
    }
}