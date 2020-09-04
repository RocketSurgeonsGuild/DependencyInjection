using System;
using Autofac;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Autofac;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.WebAssembly.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting
{
    /// <summary>
    /// Class AutofacRocketHostExtensions.
    /// </summary>
    [PublicAPI]
    public static class WebAssemblyAutofacRocketHostExtensions
    {
        /// <summary>
        /// Uses the Autofac.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="delegate">The container.</param>
        /// <returns>IHostBuilder.</returns>
        public static ConventionContextBuilder ConfigureAutofac([NotNull] this ConventionContextBuilder builder, AutofacConvention @delegate)
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
        /// <returns>IWebAssemblyHostBuilder.</returns>
        public static ConventionContextBuilder UseAutofac([NotNull] this ConventionContextBuilder builder, ContainerBuilder? containerBuilder = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ConfigureHosting((context, builder) => builder.ConfigureContainer(new AutofacConventionServiceProviderFactory(context, containerBuilder)));
        }
    }
}