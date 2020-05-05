using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ConnectingApps.TestEnablers
{
    public abstract class RefitClientTestBase<TStartup,TRefitClient> : IClassFixture<CustomWebApplicationFactory<TStartup>> where TStartup : class where TRefitClient : class
    {
        private HttpClient _httpClient;
        protected readonly TRefitClient RefitClient;

        protected RefitClientTestBase(CustomWebApplicationFactory<TStartup> factory, int portNumber, bool useHttps)
        {
            var extraConfiguration = GetConfiguration();
            string afterHttp = useHttps ? "s" : "";
            _httpClient = factory.WithWebHostBuilder(whb =>
            {
                whb.ConfigureAppConfiguration((context, configbuilder) =>
                {
                    configbuilder.AddInMemoryCollection(extraConfiguration);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http{afterHttp}://localhost:{portNumber}")
            });
            RefitClient = CreateRefitClient(_httpClient);
        }

        protected abstract TRefitClient CreateRefitClient(HttpClient httpClient);

        protected virtual Dictionary<string, string> GetConfiguration()
        {
            return new Dictionary<string, string>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
