using Vogen;

namespace Backend.Domains.Common.Domain.VO;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson | Conversions.EfCoreValueConverter)]
public partial class Description
{
    private static string NormalizeInput(string input)
    {
        return input;
    }

    private static Validation Validate(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("Key cannot be empty")
            : input.Length > 500
                ? Validation.Invalid("Description cannot be longer than 100 characters")
                : Validation.Ok;
    }
}
