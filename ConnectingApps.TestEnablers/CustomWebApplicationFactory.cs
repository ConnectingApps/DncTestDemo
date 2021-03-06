﻿using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectingApps.TestEnablers
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var types = typeof(TStartup).Assembly.GetTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).ToList();
            foreach (var type in types)
            {
                builder.ConfigureServices(sc => sc.AddTransient(type, type));
            }
            base.ConfigureWebHost(builder);
        }
    }
}
