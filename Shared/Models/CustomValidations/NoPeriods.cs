using System.ComponentModel.DataAnnotations;

namespace Shared.Models.CustomValidations
{
    public sealed class NoPeriods : ValidationAttribute
    {
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool IsValid(object value)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string input = value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            bool noPeriods = input.Contains('.') == false;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return noPeriods;
        }
    }
}
