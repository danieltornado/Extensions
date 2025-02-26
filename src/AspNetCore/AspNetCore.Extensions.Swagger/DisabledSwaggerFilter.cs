using System.Reflection;
using JetBrains.Annotations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCore.Extensions.Swagger;

/// <summary>
/// Sets all methods as deprecated which have <see cref="DisabledActionAttribute"/>
/// </summary>
[PublicAPI]
public sealed class DisabledSwaggerFilter : IOperationFilter
{
    #region Implementation of IOperationFilter

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo.GetCustomAttributes<DisabledActionAttribute>();
        if (attributes.Any())
        {
            operation.Deprecated = true;
        }
    }

    #endregion
}