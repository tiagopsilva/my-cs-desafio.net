using System.Web.Http;
using AuthAPI.Domain.Commands.Handlers;
using AuthAPI.Domain.Repositories.Interfaces;
using AuthAPI.Domain.Services;
using AuthAPI.Domain.Services.Interfaces;
using AuthAPI.Domain.Validations;
using AuthAPI.Infra.Data.ADO.NET;
using AuthAPI.Infra.Data.EF.EFContext;
using AuthAPI.Infra.Data.EF.Repositories;
using AuthAPI.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace AuthAPI.WebApi.Configuration
{
    public static class SimpleInjectorConfig
    {
        public static Container Configure(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			SetEntries(container);

            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static void SetEntries(Container container)
        {
            container.Register<AuthAPIContext>(Lifestyle.Scoped);

            container.Register<AuthApiDbConnection>(Lifestyle.Scoped);

            // commands
            container.Register<UsuarioCommandHandler>(Lifestyle.Transient);

            // domain services
            container.Register<UsuarioService>(Lifestyle.Transient);

            // other services
            container.Register<ITokenService, AuthService>();

            // repositories
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Transient);
            container.Register<ISignUpRepository, SignUpRepository>();

            // validators
            container.Register<EmailValidator>(Lifestyle.Singleton);
            container.Register<UsuarioValidator>(Lifestyle.Singleton);
            container.Register<TelefoneValidator>(Lifestyle.Singleton);
        }
    }
}