using System.Reflection;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Extensions.Validation;

/// <summary>
/// Validates parameters marked by <see cref="ValidateAttribute"/>
/// </summary>
[PublicAPI]
public class ValidationActionFilter : IActionFilter
{
    #region Implementation of IActionFilter

    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var parameterDescriptor in context.ActionDescriptor.Parameters.OfType<ControllerParameterDescriptor>())
        {
            var validateAttribute = parameterDescriptor.ParameterInfo.GetCustomAttribute(typeof(ValidateAttribute));
            if (validateAttribute is ValidateAttribute validate)
            {
                var validator = (IValidator)context.HttpContext.RequestServices.GetRequiredService(validate.ValidatorType);
                var validationResult = validator.Validate(new ValidationContext<object>(context.ActionArguments[parameterDescriptor.Name]!));
                if (validationResult.IsValid is false)
                {
                    context.Result = new JsonResult(validationResult.Errors);
                    return;
                }
            }
        }
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // nothing
    }

    #endregion
}