using System.Linq;

namespace Proverb.Api.Core.Helpers
{
    public static class SaveHelpers
    {
        public static ValidationMessages ToValidationMessages(
            this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, bool camelCaseKeyName = true) 
        {
            var errors = modelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    kvp => CamelCasePropNames(kvp.Key),
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage)
                );

            return new ValidationMessages(errors);
        }

        public static ValidationMessages WithCamelCaseKeys(this ValidationMessages validationMessages)
        {
            var errors = validationMessages.Errors
                .ToDictionary(
                    kvp => CamelCasePropNames(kvp.Key),
                    kvp => kvp.Value
                );

            return new ValidationMessages(errors);
        }

        public static string CamelCasePropNames(string propName) =>
            string.Join(".", 
                propName.Split('.').Select(prop => prop.Substring(0, 1).ToLower() + prop.Substring(1, prop.Length - 1))
            );
    }
}
