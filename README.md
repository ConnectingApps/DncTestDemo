# DncTestDemo
A demonstration of automatated tests for .NET Core applications

## TestEnablers

This [nuget package](https://www.nuget.org/packages/ConnectingApps.TestEnablers/) enables you to create integration tests for .NET Core. No need to mock your interfaces since that's the idea of integration testing and include `Startup` and `Program` in your code coverage. It's like unit testing but more "real".

Here is how your project file should look like:

````xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="ConnectingApps.TestEnablers" Version="3.1.1" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="3.1.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\ConnectingApps.TestDemo\ConnectingApps.TestDemo.csproj" />
  </ItemGroup>

</Project>
````

So what you just need is have `ConnectingApps.TestEnablers` `Microsoft.AspNetCore.Mvc.Testing` and `xunit.runner.visualstudio` . Logically, you need a reference to your web project (the one with the `Startup` class) too. 

To ensure all dependencies are there, you can type:

````bash
dotnet restore
````

but if you do a restore in Rider or Visual Studio, that is fine too.

Now it is time to write the c# code that does the real work. 

````csharp
    public class WeatherForecastControllerTests :  IntegrationTestBase<Startup, WeatherForecastController>
    {
        private WeatherForecastController _weatherForecastController;

        public WeatherForecastControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void VerifyGetter()
        {
            var output = _weatherForecastController.Get();
            Assert.Equal(5, output.Count());
        }

        protected override void SetTestInstance(WeatherForecastController testInstance)
        {
            _weatherForecastController = testInstance;
        }

        protected override Dictionary<string, string> GetConfiguration()
        {
            var configuration = base.GetConfiguration();
            configuration.Add("AppSettingsKey","AppSettingsValue");
            return configuration;
        }
    }

````

As you can see, to inherit from `IntegrationTestBase`, we need to specify the `Startup` class and the class we want to write our integration tests for. For each test, the test instance is set for you by a call to `SetTestInstance` and you can, for each test, modify or add appsettings with `GetConfiguration`.

A directly working example can be found [here](https://github.com/ConnectingApps/DncTestDemo/tree/master/ConnectingApps.IntegrationTests.NugetExample). If you prefer to write all the boiler plate code yourself or contribute to our boilerplate code, read [this](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1).


