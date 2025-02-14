using System.Text.RegularExpressions;
using Vogen;
using Generator = PasswordGenerator.Password;
using Crypt = BCrypt.Net.BCrypt;

namespace Backend.Domains.User.Domain.VO;

[ValueObject<string>]
public partial class Password
{
    public Hash ToHash()
    {
        return Hash.From(Crypt.HashPassword(Value));
    }

    private static string NormalizeInput(string input)
    {
        return input;
    }

    private static Validation Validate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Validation.Invalid("Password cannot be empty.");
        }

        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");

        return !regex.IsMatch(input)
            ? Validation.Invalid("Password must contain at least one lowercase letter, one uppercase letter, one digit, one special character, and be at least 8 characters long.")
            : Validation.Ok;
    }

    public static Password Generate()
    {
        var generator = new Generator(true, true, true, true, 8);

        string password;
        do
        {
            password = generator.Next();
        }
        while (string.IsNullOrWhiteSpace(password));

        return From(password);
    }
}
