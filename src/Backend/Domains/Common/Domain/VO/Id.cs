using Vogen;

namespace Backend.Domains.Common.Domain.VO;

[ValueObject<Guid>(conversions: Conversions.NewtonsoftJson | Conversions.EfCoreValueConverter)]
public partial class Id
{
    public static readonly Id Empty = new Id(Guid.Empty);

    private static Validation Validate(Guid input)
    {
        return input == Guid.Empty
            ? Validation.Invalid("Id cannot be empty")
            : Validation.Ok;
    }

    private static Guid NormalizeInput(Guid input)
    {
        return input;
    }
}
