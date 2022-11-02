namespace IdentityApi.Models.Api;

using FluentValidation.Results;

/// <summary>
/// Interface for models with validation.
/// </summary>
public interface IValidatedModel
{
    /// <summary>
    /// Method for validating model.
    /// </summary>
    /// <returns>Result of a validation.</returns>
    public ValidationResult Validate();
}