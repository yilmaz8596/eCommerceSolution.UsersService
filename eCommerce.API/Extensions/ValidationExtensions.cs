using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Extensions
{
    public static class ValidationExtensions
    {
        public static async Task<IActionResult?> ValidateAndReturnErrorsAsync<T>(
            this IValidator<T> validator,
            T model)
        {
            ValidationResult validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var response = new
                {
                    Status = 400,
                    Title = "Validation Failed",
                    Errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                )
                };
                return new BadRequestObjectResult(response);
            }

            return null;
        }
    }
}