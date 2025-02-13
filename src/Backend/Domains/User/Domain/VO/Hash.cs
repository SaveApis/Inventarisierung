using Vogen;

namespace Backend.Domains.User.Domain.VO;

[ValueObject<string>(Conversions.NewtonsoftJson | Conversions.EfCoreValueConverter)]
public partial class Hash
{
    private static string NormalizeInput(string input)
    {
        return input;
    }

    private static Validation Validate(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("Hash cannot be empty.")
            : input.Length != 60
                ? Validation.Invalid("Hash must be 60 characters long.")
                : Validation.Ok;
    }
}
