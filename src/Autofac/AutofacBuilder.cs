﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Hosting;

namespace Rocket.Surgery.Extensions.Autofac
{
    public class AutofacBuilder : AutofacBuilderBase
    {
        public AutofacBuilder(
            IConventionScanner scanner,
            IAssemblyProvider assemblyProvider,
            IAssemblyCandidateFinder assemblyCandidateFinder,
            IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment) :
            base(scanner, assemblyProvider, assemblyCandidateFinder, services, configuration, environment){ }

        /// <summary>
        /// Builds the root container, and returns the lifetime scopes for the application and system containers
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public IContainer Build(ContainerBuilder containerBuilder, ILogger logger)
        {
            new ConventionComposer(_scanner, logger)
                .Register(
                this,
                typeof(IServiceConvention),
                typeof(IAutofacConvention),
                typeof(ServiceConventionDelegate),
                typeof(AutofacConventionDelegate));

            _core.Collection.Apply(containerBuilder);
            containerBuilder.Populate(Services);

            _application.Collection.Apply(containerBuilder);
            containerBuilder.Populate(_application.Services);

            var result = containerBuilder.Build();
            var sp = new AutofacServiceProvider(result);
            _application.LifetimeScopeOnBuild.Send(result);
            _application.ServiceProviderOnBuild.Send(sp);

            _core.LifetimeScopeOnBuild.Send(result);
            _core.ServiceProviderOnBuild.Send(sp);

            _containerObservable.Send(result);

            return result;
        }
    }
}
