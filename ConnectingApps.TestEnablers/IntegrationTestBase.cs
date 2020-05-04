using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ConnectingApps.TestEnablers
{
    public abstract class IntegrationTestBase<TStartup,TTestType> : IDisposable, IClassFixture<CustomWebApplicationFactory<TStartup>> where TStartup : class where TTestType : class
    {
        protected readonly HttpClient HttpClient;

        protected IntegrationTestBase(CustomWebApplicationFactory<TStartup> factory)
        {
            HttpClient = factory.WithWebHostBuilder(whb =>
            {
                whb.ConfigureAppConfiguration((context, configbuilder) =>
                {
                    configbuilder.AddInMemoryCollection(null);
                });
                whb.ConfigureTestServices(sc =>
                {
                    var scope = sc.BuildServiceProvider().CreateScope();
                    var testInstance = scope.ServiceProvider.GetService<TTestType>();
                    SetTestInstance(testInstance);
                });
            }).CreateClient();
        }


        protected abstract void SetTestInstance(TTestType testInstance);

        protected virtual Dictionary<string, string> GetConfiguration()
        {
            return new Dictionary<string, string>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
