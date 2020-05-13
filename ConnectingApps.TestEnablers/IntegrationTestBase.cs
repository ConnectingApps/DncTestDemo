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
        private IServiceScope _serviceScope;
        private ServiceProvider _serviceProvider;

        protected IntegrationTestBase(CustomWebApplicationFactory<TStartup> factory)
        {
            var extraConfiguration = GetConfiguration();
            HttpClient = factory.WithWebHostBuilder(whb =>
            {
                whb.ConfigureAppConfiguration((context, configbuilder) =>
                {
                    configbuilder.AddInMemoryCollection(extraConfiguration);
                });
                whb.ConfigureTestServices(sc =>
                {
                    _serviceProvider = sc.BuildServiceProvider();
                    _serviceScope = _serviceProvider.CreateScope();
                    var testInstance = _serviceScope.ServiceProvider.GetService<TTestType>();
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
                _serviceProvider.Dispose();
                _serviceScope.Dispose();
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
