using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Extensions;

/// <summary>
/// Alternative for <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute{T}"/>. Use it if you want to initialize a constructing filter with using DI by any logic
/// </summary>
/// <typeparam name="T"></typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
[PublicAPI]
public abstract class BuildFilterAttribute<T> : Attribute, IFilterFactory
    where T : IFilterMetadata
{
    private ObjectFactory? _factory;
    
    #region Implementation of IFilterFactory

    /// <inheritdoc />
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        if (_factory == null)
        {
            _factory = ActivatorUtilities.CreateFactory(typeof(T), Type.EmptyTypes);
        }

        var filter = (IFilterMetadata)_factory(serviceProvider, null);
        if (filter is IFilterFactory filterFactory)
        {
            // Unwrap filter factories
            filter = filterFactory.CreateInstance(serviceProvider);
        }

        if (filter is T tFilter)
        {
            Initialize(tFilter);
        }

        return filter;
    }

    /// <inheritdoc />
    public bool IsReusable { get; set; }

    #endregion

    /// <summary>
    /// Initializes the created filter by custom logic
    /// </summary>
    /// <param name="filter"></param>
    protected abstract void Initialize(T filter);
}