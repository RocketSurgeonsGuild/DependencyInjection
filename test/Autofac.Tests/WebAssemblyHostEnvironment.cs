using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.Autofac.Tests;

namespace Rocket.Surgery.Extensions.Autofac.Tests
{
    internal sealed class WebAssemblyHostEnvironment : IWebAssemblyHostEnvironment
    {
        public WebAssemblyHostEnvironment(string environment, string baseAddress)
        {
            Environment = environment;
            BaseAddress = baseAddress;
        }

        public string Environment { get; }

        public string BaseAddress { get; }
    }
}