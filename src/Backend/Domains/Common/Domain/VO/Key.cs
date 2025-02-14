using Vogen;

namespace Backend.Domains.Common.Domain.VO;

[ValueObject<string>(conversions: Conversions.NewtonsoftJson | Conversions.EfCoreValueConverter)]
public partial class Key
{
    public static readonly Key Empty = new Key(string.Empty);

    private static string NormalizeInput(string input)
    {
        return input.ToLower().Trim().Replace(" ", ".");
    }

    private static Validation Validate(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("Key cannot be empty")
            : input.Length > 100
                ? Validation.Invalid("Key cannot be longer than 100 characters")
                : Validation.Ok;
    }
}
