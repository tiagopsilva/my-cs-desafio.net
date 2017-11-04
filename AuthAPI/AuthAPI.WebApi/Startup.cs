using System.Globalization;
using System.Web.Http;
using AuthAPI.WebApi.Configuration;
using FluentValidation;
using Owin;
using Microsoft.Owin;
using SimpleInjector;

[assembly: OwinStartup(typeof(AuthAPI.WebApi.Startup))]
namespace AuthAPI.WebApi
{
    public class Startup
    {
        public static Container Container;

        public void Configuration(IAppBuilder app)
        {
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");

            var config = new HttpConfiguration();

            Container = SimpleInjectorConfig.Configure(config);
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
