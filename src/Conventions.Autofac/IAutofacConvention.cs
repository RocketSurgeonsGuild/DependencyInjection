using JetBrains.Annotations;

namespace Rocket.Surgery.Conventions.Autofac
{
    /// <summary>
    /// IAutofacConvention
    /// Implements the <see cref="IConvention{TContext}" />
    /// </summary>
    /// <seealso cref="IConvention{IAutofacConventionContext}" />
    [PublicAPI]
    public interface IAutofacConvention : IConvention<IAutofacConventionContext> { }
}