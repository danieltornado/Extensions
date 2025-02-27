using System.Diagnostics;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
                    context.Result = CreateResult(context, validationResult);
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

    private IActionResult CreateResult(ActionExecutingContext context, ValidationResult validationResult)
    {
        ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred.",
            Detail = "One or more validation errors occurred.",
            Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}",
            Type = "validationException",
        };

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        problemDetails.Extensions.TryAdd("traceId", activity?.Id);

        problemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        problemDetails.Extensions.TryAdd("errors",
            validationResult
                .Errors
                .ToLookup(e => e.PropertyName)
                .ToDictionary(e => e.Key, e => e.Select(x => x.ErrorMessage).ToList()));

        return new JsonResult(problemDetails)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}