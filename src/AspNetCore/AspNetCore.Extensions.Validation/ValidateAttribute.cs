using FluentValidation;
using JetBrains.Annotations;

namespace AspNetCore.Extensions.Validation;

/// <summary>
/// Marks a parameter as validatable
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
[PublicAPI]
public class ValidateAttribute : Attribute
{
    /// <summary>
    /// Main constructor
    /// </summary>
    /// <param name="validatorType">Type of validator</param>
    public ValidateAttribute(Type validatorType)
    {
        ValidatorType = validatorType;
    }

    public Type ValidatorType { get; }
}

/// <summary>
/// Marks a parameter as validatable
/// </summary>
[PublicAPI]
public sealed class ValidateAttribute<TValidator> : ValidateAttribute
    where TValidator : IValidator
{
    /// <summary>
    /// Main constructor
    /// </summary>
    public ValidateAttribute()
        : base(typeof(TValidator))
    {
    }
}