﻿using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Rocket.Surgery.Extensions.Autofac
{
    public interface IServiceAndContainerWrapper
    {
        IAutofacConventionContext ConfigureContainer(ContainerBuilderDelegate builder);
        IServiceCollection Services { get; }
        IObservable<ILifetimeScope> OnBuild { get; }
    }
}
