using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hans.UserSample.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyName);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (propertyValue == null)
                return ValidationResult.Success;

            if (propertyValue.ToString().ToUpper() == "SUPERUSER")
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
