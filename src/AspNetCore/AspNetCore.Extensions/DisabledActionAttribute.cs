using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Extensions;

/// <summary>
/// Отключает возможность исполнения метода
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
[PublicAPI]
public class DisabledActionAttribute : Attribute, IActionFilter
{
    #region Implementation of IActionFilter

    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.Result = new ForbidResult();
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // nothing
    }

    #endregion
}