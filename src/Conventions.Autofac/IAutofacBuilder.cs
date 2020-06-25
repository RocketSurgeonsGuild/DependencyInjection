using JetBrains.Annotations;

namespace Rocket.Surgery.Conventions.Autofac
{
    /// <summary>
    /// IAutofacBuilder.
    /// Implements the <see cref="IConventionBuilder{TBuilder,TConvention,TDelegate}" />
    /// </summary>
    /// <seealso cref="IConventionBuilder{IAutofacBuilder, IAutofacConvention, AutofacConventionDelegate}" />
    [PublicAPI]
    public interface
        IAutofacBuilder : IConventionBuilder<IAutofacBuilder, IAutofacConvention, AutofacConventionDelegate> { }
}