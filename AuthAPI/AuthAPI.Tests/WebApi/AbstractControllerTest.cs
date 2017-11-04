using System;
using System.Net.Http;
using AuthAPI.WebApi;
using Microsoft.Owin.Testing;

namespace AuthAPI.Tests.WebApi
{
    public abstract class AbstractControllerTest : IDisposable
    {
        protected TestServer Server;

        protected AbstractControllerTest()
        {
            Server = TestServer.Create<Startup>();
        }

        protected HttpClient CreateClient()
        {
            return new HttpClient(Server.Handler) { BaseAddress = Server.BaseAddress };
        }

        public void Dispose()
        {
            Server?.Dispose();
            Server = null;
            GC.SuppressFinalize(this);
        }
    }
}