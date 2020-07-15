using System;
using Autofac;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Autofac;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Class AutofacRocketHostExtensions.
    /// </summary>
    [PublicAPI]
    public static class AutofacRocketHostExtensions
    {
        /// <summary>
        /// Uses the Autofac.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="@delegate">The container.</param>
        /// <returns>IHostBuilder.</returns>
        public static IConventionHostBuilder ConfigureAutofac([NotNull] this IConventionHostBuilder builder, AutofacConventionDelegate @delegate)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AppendDelegate(@delegate);
            return builder;
        }

        /// <summary>
        /// Uses the autofac.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="containerBuilder">The container builder.</param>
        /// <returns>IConventionHostBuilder.</returns>
        public static IConventionHostBuilder UseAutofac(
            [NotNull] this IConventionHostBuilder builder,
            ContainerBuilder? containerBuilder = null
        )
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ConfigureHosting(
                hosting => hosting.Builder.UseServiceProviderFactory(
                    context =>
                        new ServicesBuilderServiceProviderFactory(
                            collection =>
                                new AutofacBuilder(
                                    context.HostingEnvironment,
                                    context.Configuration,
                                    builder.Get<IConventionScanner>(),
                                    builder.Get<IAssemblyProvider>(),
                                    builder.Get<IAssemblyCandidateFinder>(),
                                    collection,
                                    containerBuilder ?? new ContainerBuilder(),
                                    builder.Get<ILogger>(),
                                    builder.ServiceProperties
                                )
                        )
                )
            );
        }
    }
}