using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.Autofac.Tests;

namespace Rocket.Surgery.Extensions.Autofac.Tests
{
    internal class LoggingBuilder : ILoggingBuilder
    {
        public LoggingBuilder(IServiceCollection services) => Services = services;

        public IServiceCollection Services { get; }
    }
}