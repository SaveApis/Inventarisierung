using System.ComponentModel.DataAnnotations;
using Vogen;

namespace Backend.Domains.User.Domain.VO;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson | Conversions.EfCoreValueConverter)]
public partial class Email
{
    private static string NormalizeInput(string input)
    {
        return input;
    }

    private static Validation Validate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Validation.Invalid("Email cannot be empty.");
        }

        return new EmailAddressAttribute().IsValid(input)
            ? Validation.Ok
            : Validation.Invalid("Email is not valid.");
    }
}
