using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using FakeItEasy;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Autofac;
using Rocket.Surgery.Conventions.CommandLine;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.Extensions.Autofac.Tests;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.WebAssembly.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Rocket.Surgery.Extensions.Autofac.Tests
{
    public class WebAssemblyAutofacBuilderTests : AutoFakeTest
    {
        [Fact]
        public async Task Should_Integrate_With_Autofac()
        {
            var builder = LocalWebAssemblyHostBuilder.CreateDefault()
               .ConfigureRocketSurgery(
                    rb => rb
                       .UseScannerUnsafe(new BasicConventionScanner(A.Fake<IServiceProviderDictionary>()))
                       .UseAutofac()
                       .UseAssemblyCandidateFinder(
                            new DefaultAssemblyCandidateFinder(new[] { typeof(WebAssemblyAutofacBuilderTests).Assembly })
                        )
                       .UseAssemblyProvider(new DefaultAssemblyProvider(new[] { typeof(WebAssemblyAutofacBuilderTests).Assembly }))
                       .AppendDelegate(
                            new CommandLineConventionDelegate(c => c.OnRun(state => 1337)),
                            new CommandLineConventionDelegate(c => c.OnRun(state => 1337))
                        )
                );

            var was = builder.Build();
            was.Services.GetRequiredService<ILifetimeScope>().Should().NotBeNull();
        }

        public WebAssemblyAutofacBuilderTests(ITestOutputHelper outputHelper) : base(outputHelper) { }
    }
}